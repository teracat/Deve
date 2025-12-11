namespace Deve.Dto
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

        public Result(ResultErrorType errorType, string? fieldName, string? errorDescription)
        {
            Success = false;
            Errors = [new ResultError(errorType, fieldName, errorDescription)];
        }

        public Result(ResultErrorType errorType, string? fieldName)
        {
            Success = false;
            Errors = [new ResultError(errorType, fieldName)];
        }

        public Result(ResultErrorType errorType)
        {
            Success = false;
            Errors = [new ResultError(errorType)];
        }

        public Result(IList<ResultError> errors)
        {
            Success = errors.Count == 0;
            Errors = errors;
        }
    }
}