namespace Pretty.Services.Dto
{
    public class ActResult<T>
    {
        public ActResult(StatusCode statusCode, string result)
        {
            StatusCode = statusCode;
            Result = result;
        }

        public ActResult(StatusCode statusCode, string result, T extends)
        {
            StatusCode = statusCode;
            Result = result;
            Extends = extends;
        }

        public StatusCode StatusCode { get; set; }

        public string Result { get; set; }

        public T Extends { get; set; }
    }
}
