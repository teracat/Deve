namespace Deve.Model
{
    public class ResultGet<T> : Result
    {
        public T? Data { get; set; }

        public ResultGet()
            : base()
        {
            Data = default;
        }

        public ResultGet(T data)
            : base()
        {
            Data = data;
        }

        public ResultGet(Result result)
            : base(result)
        {
        }

        public ResultGet(IList<ResultError> errors)
            : base(errors)
        {
        }

        public ResultGet(ResultErrorType errorType, string? fieldName = null, string? errorDescription = null)
            : base(errorType, fieldName, errorDescription)
        {
            Data = default;
        }
    }
}
