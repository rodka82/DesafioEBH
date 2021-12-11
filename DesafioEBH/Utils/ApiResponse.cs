using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public class ApiResponse : IApiResponse
    {
        public object Result { get; set; }
        public bool IsValid { get; set; }
        public List<string> Messages { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse()
        {
            Messages = new List<string>();
        }
    }
}
