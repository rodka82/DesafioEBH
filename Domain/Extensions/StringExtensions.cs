using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNull(this string value)
        {
            return value == null;
        }

        public static bool IsEmpty(this string value)
        {
            return value.Trim() == string.Empty;
        }
    }
}
