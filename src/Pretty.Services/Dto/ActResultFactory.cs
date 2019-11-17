namespace Pretty.Services.Dto
{
    public abstract class ActResultFactory
    {
        public static ActResult<string> CreateOkActResult()
        {
            return new ActResult<string>(StatusCode.Success, "成功");
        }

        public static ActResult<string> CreateFailedActResult()
        {
            return new ActResult<string>(StatusCode.Failed, "失败");
        }
        public static ActResult<string> CreateFailedActResult(string msg)
        {
            return new ActResult<string>(StatusCode.Failed, msg);
        }

        public static ActResult<string> GetActResult(int result)
        {
            return result > 0 ? CreateOkActResult() : CreateFailedActResult();
        }
    }
}
