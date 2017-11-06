using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Book
{
    public class Log
    {
        private static readonly string fileName = "data/log.txt";
        public static void Info(string msg)
        {
            File.AppendAllLines(fileName,new string[] { msg },Encoding.UTF8);
        }
    }
}
