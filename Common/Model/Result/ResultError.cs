namespace Deve.Model
{
    public class ResultError
    {
        public ResultErrorType Type { get; set; }
        public string? Description { get; set; }
        public string? FieldName { get; set; }

        public ResultError()
        {
        }

        public ResultError(ResultErrorType type, string? fieldName, string? description)
        {
            Type = type;
            FieldName = fieldName;
            Description = description;
        }

        public ResultError(ResultErrorType type, string? fieldName)
        {
            Type = type;
            FieldName = fieldName;
        }

        public ResultError(ResultErrorType type)
        {
            Type = type;
        }
    }
}