using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;

namespace Deve.Clients.Wpf.Helpers;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
internal sealed class GreaterThanOrEqualAttribute : ValidationAttribute
{
    public long MinValue { get; }
    public string? PropertyName { get; }

    public GreaterThanOrEqualAttribute(int minValue)
    {
        MinValue = minValue;
    }

    public GreaterThanOrEqualAttribute(string propertyName, int minValue)
    {
        PropertyName = propertyName;
        MinValue = minValue;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            long? extractedValue = null;

            // If the value is an int directly
            if (value is short or int or long)
            {
                extractedValue = (long)value;
            }
            // If the value is a class and a property name is specified
            else if (PropertyName is not null)
            {
                var propertyInfo = value.GetType().GetProperty(PropertyName);
                if (propertyInfo is null)
                {
                    return new ValidationResult($"Property '{PropertyName}' does not exist in {value.GetType().Name}.");
                }

                var propertyValue = propertyInfo.GetValue(value);
                if (propertyValue is short or int or long)
                {
                    extractedValue = (long)propertyValue;
                }
            }

            // Final validation check
            if (extractedValue >= MinValue)
            {
                return ValidationResult.Success;
            }
        }

        // Get the error message (default, or from resource)
        string errorMessage = FormatErrorMessage(validationContext.DisplayName);
        return new ValidationResult(errorMessage);
    }

    public override string FormatErrorMessage(string name)
    {
        if (!string.IsNullOrEmpty(ErrorMessageResourceName) && ErrorMessageResourceType != null)
        {
            var resource = new ResourceManager(ErrorMessageResourceType);
            string? resourceMessage = resource.GetString(ErrorMessageResourceName, CultureInfo.CurrentCulture);
            if (!string.IsNullOrEmpty(resourceMessage))
            {
                return string.Format(CultureInfo.CurrentCulture, resourceMessage, name, MinValue);
            }
        }

        return string.Format(CultureInfo.CurrentCulture, ErrorMessage ?? "The value of {0} must be at least {1}.", name, MinValue);
    }
}
