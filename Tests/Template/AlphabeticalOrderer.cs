using Xunit.Sdk;

namespace Deve.Tests.Template;

public sealed class AlphabeticalOrderer : ITestMethodOrderer
{
    public IReadOnlyCollection<TTestMethod?> OrderTestMethods<TTestMethod>(IReadOnlyCollection<TTestMethod?> testMethods) where TTestMethod : notnull, ITestMethod =>
        testMethods.OrderBy(m => m?.MethodName, StringComparer.OrdinalIgnoreCase)
                   .ToArray();
}
