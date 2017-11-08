using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Common
{
    public static class Extension
    {
        public static string Replace(this string value, string newValue,string[] oldValue)
        {
            return Kit.Replace(value, newValue, oldValue);
        }
    }
}
