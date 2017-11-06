using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Model;
using Book.Data;
using Book.Common;
using System.Net.Http;

namespace Book
{
    /// <summary>
    /// 书籍更新器
    /// </summary>
    public class BookRefresh
    {
        private UserInfo userinfo;
        private List<BookInfo> books;
        public BookRefresh()
        {
            userinfo = UserInfoHelper.GetUserInfo();
            UserBookListData ubl = new UserBookListData();
            books = ubl.GetBooks(userinfo.ID);
        }

        public async void Start()
        {
            HttpClient client = new HttpClient();
            foreach (var item in books)
            {

                List<ChapterInfo> chapters = new ChapterInfoData(item).GetChapterInfos();
            }
        }
    }
}
