using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Model
{
    /// <summary>
    /// 书籍信息
    /// </summary>
    public class BookInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 书籍网络路径
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 封面图片路径
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 最新章节
        /// </summary>
        public string Loeva { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 章节文件路径
        /// </summary>
        public string ChapterPath { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string Metadata { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(BookInfo))
            {
                return false;
            }

            BookInfo book = obj as BookInfo;
            if (Name == book.Name && Author == book.Author)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
