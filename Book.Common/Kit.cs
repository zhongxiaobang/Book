using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Book.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Kit
    {
        public static Session Session
        {
            get
            {
                return Session.GetInstance();
            }
        }
        /// <summary>
        /// 将对象或对象集合转为json字符串
        /// </summary>
        /// <param name="value">对象或对象集合</param>
        /// <param name="isFormatting">是否格式化</param>
        /// <returns></returns>
        public static string ToJson(object value,bool isFormatting = false)
        {
            return JsonConvert.SerializeObject(value, isFormatting ? Formatting.Indented : Formatting.None);
        }

        /// <summary>
        /// 将json字符串转为对象或对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static string Replace(string value, string newValue, params string[] oldValue)
        {
            for (int i = 0; i < oldValue.Length; i++)
            {
                value = value.Replace(oldValue[i], newValue);
            }
            return value;
        }
    }
}
