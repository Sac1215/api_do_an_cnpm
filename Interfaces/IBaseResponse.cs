using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Responses
{
    public interface IBaseResponse<T>
    {
        T? data { get; set; }
        string message { get; set; }
        int code { get; set; }
    }
}