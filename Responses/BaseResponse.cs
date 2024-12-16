using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Responses
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T? data { get; set; }
        public string message { get; set; }
        public int code { get; set; }

        public BaseResponse(T? data, string message, int code)
        {
            this.data = data;
            this.message = message;
            this.code = code;
        }
    }
}