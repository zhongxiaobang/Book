using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using NSoup;
using NSoup.Select;
using Book.Model;
using Book.Common;
using Book.Data;

namespace Book
{
    public partial class FrmSearch : Form
    {
        public FrmMain frmMain;
        public FrmSearch()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            try
            {
                string url = string.Format("http://zhannei.baidu.com/cse/search?s=1393206249994657467&q={0}", textBox1.Text);
                HttpClient client = new HttpClient();
                string html = await client.GetStringAsync(url);

                //List<BookInfo> books = new List<BookInfo>();

                Elements elements = NSoupClient.Parse(html).Select("div.result-list div.result-item");

                foreach (var item in elements)
                {
                    BookInfo bookInfo = new BookInfo();
                    bookInfo.ID = Kit.GetGuid();
                    bookInfo.ImagePath = item.Select("img[src]").Attr("src");
                    bookInfo.URL = item.Select("h3 a[cpos=title]").Attr("href");
                    bookInfo.Name = item.Select("h3 a[cpos=title]").Html()
                                    .Replace("em", "")
                                    .Replace("<", "")
                                    .Replace(">", "")
                                    .Replace("/", "")
                                    .Replace(" ", "");
                    bookInfo.Author = item.Select("p.result-game-item-info-tag:eq(0) span:eq(1)").Html()
                                      .Replace(" ", "");
                    bookInfo.UpdateTime = Convert.ToDateTime(item.Select("p.result-game-item-info-tag:eq(2) span:eq(1)").Html()
                                      .Replace(" ", ""));
                    bookInfo.Loeva = item.Select("p.result-game-item-info-tag:eq(3) a").Html()
                                      .Replace(" ", "");
                    bookInfo.Metadata = item.Html();
                    bookInfo.ChapterPath = "data/BookList/" + bookInfo.Author + "-" + bookInfo.Name + ".json";
                    //books.Add(bookInfo);

                    ListViewItem viewItem = new ListViewItem(bookInfo.Name);
                    viewItem.SubItems.Add(bookInfo.Author);
                    viewItem.SubItems.Add(bookInfo.UpdateTime.ToString("yyyy-MM-dd"));
                    viewItem.SubItems.Add(bookInfo.Loeva);
                    viewItem.Tag = bookInfo;
                    listView1.Items.Add(viewItem);

                }
            }
            catch (HttpRequestException httpRequestException)
            {
                Log.Info(httpRequestException.Message);

                if (MessageBox.Show("查询失败,是否重试", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    button1_Click(null,null);
                }
            }
            

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                BookInfo book = listView1.SelectedItems[0].Tag as BookInfo;
                FrmDownload frmDownload = new FrmDownload(book);
                frmDownload.ShowDialog();

                UserBookListData ubld = new UserBookListData();

                UserInfo userInfo = UserInfoHelper.GetUserInfo();

                if (ubld.GetUserBookLists(t => t.BookID == book.ID && t.UserID == userInfo.ID).Count <= 0)
                {
                    BookInfoData bif = new BookInfoData();
                    BookInfo bookInfo = bif.GetBookInfos(t => book.Equals(t)).FirstOrDefault();
                    if (bookInfo == null)
                    {
                        ubld.Add(userInfo.ID, book.ID);
                    }
                    else
                    {
                        ubld.Add(userInfo.ID, bookInfo.ID);
                    }

                }
                //刷新我的书架
                frmMain.LoadListViewData();
            }
        }

        private void 查看封面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                BookInfo book = listView1.SelectedItems[0].Tag as BookInfo;
                FrmCover fc = new FrmCover(book);
                fc.Show();
            }

        }
    }
}
