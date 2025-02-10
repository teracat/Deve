namespace Deve.Model
{
    public class ResultGetList<T> : Result
    {
        public IList<T> Data { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string OrderBy { get; set; } = string.Empty;
        public int TotalCount { get; set; }

        public ResultGetList()
            : base()
        {
            Data = [];
        }

        public ResultGetList(Result result)
            : base(result)
        {
            Data = [];
        }

        public ResultGetList(IList<T> data, int? offset, int? limit, string orderBy, int totalCount)
            : base()
        {
            Data = data;
            Offset = offset;
            Limit = limit;
            OrderBy = orderBy;
            TotalCount = totalCount;
        }

        public ResultGetList(ResultErrorType errorType, string? fieldName = null, string? errorDescription = null)
            : base(errorType, fieldName, errorDescription)
        {
            Data = [];
        }

        public ResultGetList(IList<ResultError> errors)
            : base(errors)
        {
            Data = [];
        }
    }
}
