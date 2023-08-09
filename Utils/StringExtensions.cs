using System;

namespace Utils
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string @string)
        {
            return string.IsNullOrEmpty(@string);
        }
    }
}