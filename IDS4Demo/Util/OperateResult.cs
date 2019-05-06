namespace IDS4Demo
{
    public class OperateResult
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Succeeded { get; set; }

        public static OperateResult Success = new OperateResult()
        {
            Succeeded = true
        };

        public static OperateResult Failed(string code, string description)
        {
            return new OperateResult()
            {
                Succeeded = false,
                Code = code,
                Description = description
            };
        }

        public static OperateResult<T> Failed<T>(string code, string description)
        {
            return new OperateResult<T>()
            {
                Succeeded = false,
                Code = code,
                Description = description
            };
        }

        public static OperateResult<T> Successed<T>(T data)
        {
            return new OperateResult<T>()
            {
                Succeeded = true,
                Data = data
            };
        }
    }

    public class OperateResult<T> : OperateResult
    {
        public T Data { get; set; }
    }
}
