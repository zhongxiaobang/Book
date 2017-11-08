using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSoup.Nodes;
using NSoup.Select;
using Book.Model;
using Book.Common;

namespace Book
{
    /// <summary>
    /// 解析器
    /// </summary>
    public class BookResolver
    {
        /// <summary>
        /// 获得书籍信息
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static BookInfo GetBookInfo(Element element)
        {
            BookInfo bookInfo = new BookInfo();
            bookInfo.ID = Kit.GetGuid();
            bookInfo.ImagePath = element.Select("img[src]").Attr("src");
            bookInfo.URL = element.Select("h3 a[cpos=title]").Attr("href");
            bookInfo.Name = element.Select("h3 a[cpos=title]").Html().Replace("",new string[] { " ", "<em>", "</em>" });
            bookInfo.Author = element.Select("p.result-game-item-info-tag:eq(0) span:eq(1)").Html()
                              .Replace(" ", "");
            bookInfo.UpdateTime = Convert.ToDateTime(element.Select("p.result-game-item-info-tag:eq(2) span:eq(1)").Html()
                              .Replace(" ", ""));
            bookInfo.Loeva = element.Select("p.result-game-item-info-tag:eq(3) a").Html()
                              .Replace(" ", "");
            bookInfo.Metadata = element.Html();
            bookInfo.ChapterPath = "data/BookList/" + bookInfo.Author + "-" + bookInfo.Name + ".json";
            return bookInfo;
        }
    }
}
