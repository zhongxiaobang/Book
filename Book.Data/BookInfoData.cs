using Book.Common;
using Book.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data
{
    public class BookInfoData
    {
        /// <summary>
        /// 获得书籍信息
        /// </summary>
        /// <returns></returns>
        public List<BookInfo> GetBookInfos()
        {
            if (File.Exists("data/Book.json"))
            {
                string json = FileUtils.ReadAllText("data/Book.json", Encoding.UTF8);
                return JsonConvert.DeserializeObject<List<BookInfo>>(json);
            }
            return new List<BookInfo>();
        }

        /// <summary>
        /// 获得特定条件的书籍信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<BookInfo> GetBookInfos(Func<BookInfo, bool> predicate)
        {
            return GetBookInfos().Where(predicate).ToList();
        }

        /// <summary>
        /// 新增书籍
        /// </summary>
        /// <param name="bookInfo"></param>
        public void Add(params BookInfo[] bookInfo)
        {
            List<BookInfo> bookInfos = GetBookInfos();
            bookInfos.AddRange(bookInfo);
            Save(bookInfos);
        }

        /// <summary>
        /// 删除符合条件的书籍
        /// </summary>
        /// <param name="match"></param>
        public void Delete(Func<BookInfo, bool> predicate)
        {
            List<BookInfo> bookInfos = GetBookInfos();
            foreach (var item in bookInfos.Where(predicate))
            {
                bookInfos.Remove(item);
            }
            Save(bookInfos);
        }
        /// <summary>
        /// 保存变化
        /// </summary>
        public void Save(List<BookInfo> bookInfos)
        {
            string json = Kit.ToJson(bookInfos,true);
            FileUtils.WriteAllText("data/Book.json", json, Encoding.UTF8);
        }

    }
}
