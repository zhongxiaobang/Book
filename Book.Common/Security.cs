using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Book.Common
{
    /// <summary>
    /// 安全
    /// </summary>
    public class Security
    {
        //Des密钥
        private static byte[] desKey = Encoding.UTF8.GetBytes("book1234");
        //Des向量
        private static byte[] DesIv = Encoding.UTF8.GetBytes("book1234");

        /// <summary>
        /// 将字符串进行MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ParseMd5(string str)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DesEncrypt(string str)
        {
            using (DES des = new DESCryptoServiceProvider()
            {
                Key = desKey,
                IV = DesIv
            })
            {
                using (ICryptoTransform ct = des.CreateEncryptor())
                {
                    byte[] data = Encoding.UTF8.GetBytes(str);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Write))
                        {
                            cs.Write(data, 0, data.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DesDecrypt(string str)
        {
            using (DES des = new DESCryptoServiceProvider()
            {
                Key = desKey,
                IV = DesIv
            })
            {
                using (ICryptoTransform ct = des.CreateDecryptor())
                {
                    byte[] data = Convert.FromBase64String(str);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(data, 0, data.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
