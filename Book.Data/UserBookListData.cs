using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Common;
using Book.Model;
using System.IO;

namespace Book.Data
{
    public class UserBookListData
    {
        public List<UserBookList> GetUserBookLists()
        {
            if (File.Exists("data/UserBookList.json"))
            {
                string json = FileUtils.ReadAllText("data/UserBookList.json", Encoding.UTF8);
                return Kit.ToObject<List<UserBookList>>(json);
            }
            return new List<UserBookList>();
        }

        public List<UserBookList> GetUserBookLists(Func<UserBookList, bool> predicate)
        {
            return GetUserBookLists().Where(predicate).ToList();
        }

        public List<BookInfo> GetBooks(string userId)
        {
            List<string> bookIds = GetUserBookLists(t => t.UserID == userId)
                                   .Select(t => t.BookID)
                                   .ToList();
            BookInfoData bif = new BookInfoData();
            return bif.GetBookInfos(t => bookIds.Contains(t.ID));
        }

        public List<UserInfo> GetUsers(string bookId)
        {
            List<string> userIds = GetUserBookLists(t => t.BookID == bookId)
                                   .Select(t => t.UserID)
                                   .ToList();
            UserInfoData uif = new UserInfoData();
            return uif.GetUserInfos(t => userIds.Contains(t.ID));
        }

        public void Add(string userId, string bookId)
        {
            List<UserBookList> userBookLists = GetUserBookLists();
            UserBookList ubl = new UserBookList();
            ubl.ID = Kit.GetGuid();
            ubl.UserID = userId;
            ubl.BookID = bookId;
            userBookLists.Add(ubl);
            Save(userBookLists);
        }

        public void Delete(string userId, string bookId)
        {
            List<UserBookList> userBookLists = GetUserBookLists();
            UserBookList ubl = userBookLists.Where(t => t.UserID == userId && t.BookID == bookId).FirstOrDefault();
            userBookLists.Remove(ubl);
            Save(userBookLists);
        }

        public void Save(List<UserBookList> userBookLists)
        {
            string json = Kit.ToJson(userBookLists, true);
            FileUtils.WriteAllText("data/UserBookList.json", json, Encoding.UTF8);
        }
    }
}
