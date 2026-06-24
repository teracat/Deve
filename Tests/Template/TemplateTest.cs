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

    private static (int ExitCode, string StdOut, string StdErr) Run(string file, string args, string workingDir)
    {
        using var p = new Process
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

        return (p.ExitCode, stdout, stderr);
    }

    [Fact]
    public void Step01_UninstallTemplate()
    {
        var (exitCode, _, _) = Run(DotNetCommand, $"new uninstall \"{_fixture.TemplateRoot}\"", _fixture.TempDir);

        Assert.NotEqual(-1, exitCode); // This can fail if the template is not installed, but we want to proceed with the test anyway
    }

    [Fact]
    public void Step02_InstallTemplate()
    {
        var (exitCode, stdOut, stdErr) = Run(DotNetCommand, $"new install \"{_fixture.TemplateRoot}\"", _fixture.TempDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step03_CreateProject()
    {
        var (exitCode, stdOut, stdErr) = Run(DotNetCommand, $"new deve -n {TestProjectName}", _fixture.TempDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step04_CreateModule()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand,
            $"new deve-module -n Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step05_CreateFeatureCrud()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand,
            $"new deve-feature-crud -n Orders -S Order -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step06_AddQueryList()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand,
            $"new deve-method-query-list -n GetPending -PL Orders -S Order -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step07_AddQuerySingle()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand,
            $"new deve-method-query -n GetLast -PL Orders -S Order -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step08_AddCommand()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand,
            $"new deve-method-command -n UpdateStatus -PL Orders -S Order -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step09_AddEmptyFeature()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand,
            $"new deve-feature-empty -n Deliveries -S Delivery -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step10_AddMethodToEmptyFeature()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand,
            $"new deve-method-command -n SetDelivered -PL Deliveries -S Delivery -M Sales -P {TestProjectName} --allow-scripts yes",
            projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step11_RestoreSolution()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand, $"restore --verbosity normal {TestProjectName}.All.slnx", projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");
    }

    [Fact]
    public void Step12_BuildSolution()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand, $"build --no-restore --verbosity normal {TestProjectName}.All.slnx", projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");

    }

    [Fact]
    public void Step13_TestSolution()
    {
        var projDir = Path.Combine(_fixture.TempDir, TestProjectName);

        var (exitCode, stdOut, stdErr) = Run(DotNetCommand, $"test --no-build --verbosity normal {TestProjectName}.All.slnx", projDir);

        Assert.True(
            exitCode == 0,
            $"ExitCode: {exitCode}\n\nSTDOUT:\n{stdOut}\n\nSTDERR:\n{stdErr}");

    }
}
