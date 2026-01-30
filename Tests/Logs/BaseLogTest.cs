using Deve.Logging;

namespace Deve.Tests.Logs;

public abstract class BaseLogTest
{
    #region Fields
    private readonly ILogProvider _logProvider;
    #endregion

    #region Constructor
    protected BaseLogTest(ILogProvider logProvider)
    {
        _logProvider = logProvider;
    }
    #endregion

    #region Debug
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Debug_NullOrEmpty_NoException(string? input)
    {
        var exception = Record.Exception(() => _logProvider.Debug(input!));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_Text_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Sample text"));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_TextWithArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Some text {FirstArg}", "FirstArg"));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_TextWithMultipleArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Some text {FirstArg} {SecondArg}", "FirstArg", 2));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_TextWithObjectArg_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Some text {FirstArg}", new { Name = "Test", Value = 123 }));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_TextWithNullArg_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Some text {FirstArg}", null!));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_TextWithMultipleObjectArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Some text {FirstArg} {SecondArg}", new { Name = "Test", Value = 123 }, new List<int> { 1, 2, 3 }));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_TextWithNullAndObjectArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Some text {FirstArg} {SecondArg}", null!, new { Name = "Test", Value = 123 }));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_TextWithNullAndMultipleObjectArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Some text {FirstArg} {SecondArg} {ThirdArg}", null!, new { Name = "Test", Value = 123 }, new List<int> { 1, 2, 3 }));

        Assert.Null(exception);
    }

    [Fact]
    public void Debug_TextWithMultipleNullArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Debug("Some text {FirstArg} {SecondArg} {ThirdArg}", null!, null!, null!));

        Assert.Null(exception);
    }
    #endregion

    #region Error
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Error_NullOrEmpty_NoException(string? input)
    {
        var exception = Record.Exception(() => _logProvider.Error(input!));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_Text_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Sample text"));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_Exception_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error(new InvalidOperationException("Sample exception")));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_ExceptionAndText_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error(new InvalidOperationException("Sample exception"), "Sample text"));
        Assert.Null(exception);
    }

    [Fact]
    public void Error_NullExceptionAndText_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error(null!, "Sample text"));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_ExceptionAndNullText_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error(new InvalidOperationException("Sample exception"), null!));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_NullExceptionAndNullText_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error((Exception)null!, null!));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_TextWithArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Some text {FirstArg}", "FirstArg"));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_TextWithMultipleArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Some text {FirstArg} {SecondArg}", "FirstArg", 2));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_TextWithObjectArg_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Some text {FirstArg}", new { Name = "Test", Value = 123 }));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_TextWithNullArg_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Some text {FirstArg}", null!));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_TextWithMultipleObjectArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Some text {FirstArg} {SecondArg}", new { Name = "Test", Value = 123 }, new List<int> { 1, 2, 3 }));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_TextWithNullAndObjectArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Some text {FirstArg} {SecondArg}", null!, new { Name = "Test", Value = 123 }));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_TextWithNullAndMultipleObjectArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Some text {FirstArg} {SecondArg} {ThirdArg}", null!, new { Name = "Test", Value = 123 }, new List<int> { 1, 2, 3 }));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_TextWithMultipleNullArgs_NoException()
    {
        var exception = Record.Exception(() => _logProvider.Error("Some text {FirstArg} {SecondArg} {ThirdArg}", null!, null!, null!));

        Assert.Null(exception);
    }
    #endregion
}
