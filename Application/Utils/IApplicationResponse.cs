using System.Collections.Generic;

namespace Application.Utils
{
    public interface IApplicationResponse
    {
        public object Result { get; set; }
        bool IsValid { get; set; }
        List<string> Messages { get; set; }
    }
}