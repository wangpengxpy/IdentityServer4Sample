using System;

namespace IDS4Demo
{
    public class ApiException : Exception
    {
        public string Description { get; set; }

        public ApiException(string description)
        {
            Description = description ?? "参数不正确";
        }
    }
}
