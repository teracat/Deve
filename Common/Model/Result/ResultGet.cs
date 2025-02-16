namespace Deve.Model
{
    public class ResultGet<T> : Result
    {
        public T? Data { get; set; }

        public ResultGet()
        {
            Data = default;
        }

        public ResultGet(T data)
        {
            Data = data;
        }

        public ResultGet(Result result)
            : base(result)
        {
            Data = default;
        }

        public ResultGet(IList<ResultError> errors)
            : base(errors)
        {
            Data = default;
        }

        public ResultGet(ResultErrorType errorType, string? fieldName, string? errorDescription)
            : base(errorType, fieldName, errorDescription)
        {
            Data = default;
        }

        public ResultGet(ResultErrorType errorType, string? fieldName)
            : base(errorType, fieldName)
        {
            Data = default;
        }

        public ResultGet(ResultErrorType errorType)
            : base(errorType)
        {
            Data = default;
        }
    }
}