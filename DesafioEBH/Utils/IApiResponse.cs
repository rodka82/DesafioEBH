using Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public interface IApiResponse : IApplicationResponse
    {
        public int StatusCode { get; set; }
    }
}
