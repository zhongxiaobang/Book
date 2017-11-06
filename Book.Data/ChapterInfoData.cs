using Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Book.Common;

namespace Book.Data
{
    public class ChapterInfoData
    {
        private BookInfo book;

        public ChapterInfoData(BookInfo book)
        {
            this.book = book;            
        }

        /// <summary>
        /// 获得章节信息
        /// </summary>
        /// <returns></returns>
        public List<ChapterInfo> GetChapterInfos()
        {
            if (File.Exists(book.ChapterPath))
            {
                string json = FileUtils.ReadAllText(book.ChapterPath, Encoding.UTF8);
                return JsonConvert.DeserializeObject<List<ChapterInfo>>(json);
            }
            return new List<ChapterInfo>();
        }

        /// <summary>
        /// 获得特定条件的章节信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<ChapterInfo> GetChapterInfos(Func<ChapterInfo, bool> predicate)
        {
            return GetChapterInfos().Where(predicate).ToList();
        }

        /// <summary>
        /// 新增章节
        /// </summary>
        /// <param name="chapterInfo"></param>
        public void Add(params ChapterInfo[] chapterInfo)
        {
            List<ChapterInfo> chapterInfos = GetChapterInfos();
            chapterInfos.AddRange(chapterInfo);
            Save(chapterInfos);
        }

        /// <summary>
        /// 删除符合条件的章节
        /// </summary>
        /// <param name="match"></param>
        public void Delete(Func<ChapterInfo, bool> predicate)
        {
            List<ChapterInfo> chapterInfos = GetChapterInfos();
            foreach (var item in chapterInfos.Where(predicate))
            {
                chapterInfos.Remove(item);
            }
            Save(chapterInfos);
        }
        /// <summary>
        /// 保存变化
        /// </summary>
        public void Save(List<ChapterInfo> chapterInfos)
        {
            string json = Kit.ToJson(chapterInfos, true);
            FileUtils.WriteAllText(book.ChapterPath, json, Encoding.UTF8);
        }

        
    }
}
