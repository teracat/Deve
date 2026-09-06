namespace Deve.Tests.Template.Fixtures;

public sealed class TemplateTestFixture : IDisposable
{
    public string TempDir { get; }
    public string TemplateRoot { get; }

    public TemplateTestFixture()
    {
        TempDir = Path.Combine(Path.GetTempPath(), "DeveTemplateTests_" + Guid.NewGuid());
        _ = Directory.CreateDirectory(TempDir);

        TemplateRoot = GetTemplateRoot();
    }

    public static string GetTemplateRoot()
    {
        // Path where the test assembly is located
        var basePath = AppContext.BaseDirectory;

        // Navigate up to reach repo root
        return Path.GetFullPath(Path.Combine(basePath, "../../../../../"));
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(TempDir))
            {
                Directory.Delete(TempDir, true);
            }
        }
        catch { /* ignore cleanup errors */ }
    }
}
