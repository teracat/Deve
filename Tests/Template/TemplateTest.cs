using System.Diagnostics;
using Deve.Tests.Template.Fixtures;

namespace Deve.Tests.Template;

[TestCaseOrderer(typeof(AlphabeticalOrderer))]
public class TemplateTest : IClassFixture<TemplateTestFixture>
{
    private readonly TemplateTestFixture _fixture;
    private const string TestProjectName = "MyProject";
    private const string DotNetCommand = "dotnet";

    public TemplateTest(TemplateTestFixture fixture)
    {
        _fixture = fixture;
    }

    private static int Run(string file, string args, string workingDir)
    {
        var p = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = file,
                Arguments = args,
                WorkingDirectory = workingDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        _ = p.Start();

        string stdout = p.StandardOutput.ReadToEnd();
        string stderr = p.StandardError.ReadToEnd();

        p.WaitForExit();

        Console.WriteLine(stdout);
        Console.WriteLine(stderr);

        return p.ExitCode;
    }

    [Fact]
    public void Step01_UninstallTemplate()
    {
        var exit = Run(DotNetCommand, $"new uninstall \"{_fixture.TemplateRoot}\"", _fixture.TempDir);
        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step02_InstallTemplate()
    {
        var exit = Run(DotNetCommand, $"new install \"{_fixture.TemplateRoot}\"", _fixture.TempDir);
        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step03_CreateProject()
    {
        var exit = Run(DotNetCommand, $"new deve -n {TestProjectName}", _fixture.TempDir);
        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step04_CreateModule()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand,
            $"new deve-module -n Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step05_CreateFeatureCrud()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand,
            $"new deve-feature-crud -n Orders -S Order -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step06_AddQueryList()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand,
            $"new deve-method-query-list -n GetPending -PL Orders -S Order -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step07_AddQuerySingle()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand,
            $"new deve-method-query -n GetLast -PL Orders -S Order -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step08_AddCommand()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand,
            $"new deve-method-command -n UpdateStatus -PL Orders -S Order -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step09_AddEmptyFeature()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand,
            $"new deve-feature-empty -n Deliveries -S Delivery -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step10_AddMethodToEmptyFeature()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand,
            $"new deve-method-command -n SetDelivered -PL Deliveries -S Delivery -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step11_RestoreSolution()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand, $"restore {TestProjectName}.All.slnx", projDir);
        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step12_BuildSolution()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand, $"build --no-restore {TestProjectName}.All.slnx", projDir);
        Assert.Equal(0, exit);
    }

    [Fact]
    public void Step13_TestSolution()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);
        var exit = Run(DotNetCommand, $"test --no-build --verbosity normal {TestProjectName}.All.slnx", projDir);
        Assert.Equal(0, exit);
    }
}
