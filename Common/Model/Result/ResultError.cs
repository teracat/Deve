namespace Deve
{
    public class ResultError
    {
        public ResultErrorType Type { get; set; }
        public string? Description { get; set; }
        public string? FieldName { get; set; }

        public ResultError()
        {
        }

        public ResultError(ResultErrorType type, string? fieldName = null, string? description = null)
        {
            Type = type;
            FieldName = fieldName;
            Description = description;
        }
    }
}
