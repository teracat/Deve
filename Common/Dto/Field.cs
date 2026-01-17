using System.Runtime.CompilerServices;

namespace Deve.Dto;

public record Field
{
    public string Name { get; set; } = string.Empty;
    public object? Value { get; set; }

    public Field()
    {
    }

    public Field(string name, object? value)
    {
        Name = name;
        Value = value;
    }

    public Field(object? value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
        Value = value;
        Name = name;
    }
}
