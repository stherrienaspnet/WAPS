using System;

namespace WAPS.BookStore.Domain.Extenstions
{
    public static class StringExtensions
    {
        public static string ToSqlFormat(this String pString)
        {
            return pString.Replace("\n", "").Replace("\t", "").Replace("\r", "");
        }
    }
}
