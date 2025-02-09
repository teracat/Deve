namespace Deve.Model
{
    public class Result
    {
        public bool Success { get; set; }
        public IList<ResultError> Errors { get; set; }

        public Result()
        {
            Success = true;
            Errors = [];
        }

        public Result(Result result)
        {
            Success = result.Success;
            Errors = result.Errors;
        }

        public Result(ResultErrorType errorType, string? fieldName = null, string? errorDescription = null)
        {
            Success = false;
            Errors = [new ResultError(errorType, fieldName, errorDescription)];
        }

        public Result(IList<ResultError> errors)
        {
            Success = errors.Count == 0;
            Errors = errors;
        }
    }
}
