using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Book.Common
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public class FileUtils
    {
        public static string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }
        public static void WriteAllText(string path, string contents, Encoding encoding)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            File.WriteAllText(path, contents, encoding);
        }
    }
}
