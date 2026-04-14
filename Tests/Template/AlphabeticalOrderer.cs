using Xunit.Sdk;

namespace Deve.Tests.Template;

public sealed class AlphabeticalOrderer : ITestCaseOrderer
{
    public IReadOnlyCollection<TTestCase> OrderTestCases<TTestCase>(IReadOnlyCollection<TTestCase> testCases) where TTestCase : notnull, ITestCase
    {
        var result = testCases.Cast<IXunitTestCase>().ToList();
        result.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
        return result.Cast<TTestCase>().ToArray();
    }
}
