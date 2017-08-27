using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationApp.Models
{    
    public class RequestResult
    {
        public RequestState State { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }

    public enum RequestState
    {
        Failed = -1,
        NotAuth = 0,
        Success = 1
    }    
}
