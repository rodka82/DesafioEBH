using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Utils
{
    public class ApplicationResponse : IApplicationResponse
    {
        public object Result { get; set; }
        public bool IsValid { get; set; }
        public List<string> Messages { get; set; }

        public ApplicationResponse()
        {
            Messages = new List<string>();
        }
    }
}