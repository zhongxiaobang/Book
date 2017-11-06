using Book.Model;
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
using NSoup.Nodes;
using NSoup.Select;
using Book.Common;
using Book.Data;
using System.Text.RegularExpressions;

namespace Book
{
    public partial class FrmDownload : Form
    {
        private BookInfo book;
        public FrmDownload(BookInfo book)
        {
            InitializeComponent();
            this.book = book;
        }

        private async void FrmDownload_Load(object sender, EventArgs e)
        {
            BookInfoData bid = new BookInfoData();


            if (bid.GetBookInfos().Where(t => book.Equals(t)).FirstOrDefault() != null)
            {
                this.Close();
            }
            else
            {
                ChapterInfoData cif = new ChapterInfoData(book);

                groupBox1.Text = "正在初始化.....";
                HttpClient client = new HttpClient();
                string html = await client.GetStringAsync(book.URL);

                Match macth = Regex.Match(html, "<script>window.location.href='(.*)';</script>");
                if (macth.Length > 0 && macth.Groups.Count > 1)
                {
                    html = await client.GetStringAsync(macth.Groups[1].Value);
                }

                Elements elements = NSoupClient.Parse(html).Select("#list a[href]");
                List<ChapterInfo> chapterInfos = new List<ChapterInfo>();
                foreach (var item in elements)
                {
                    ChapterInfo chapterInfo = new ChapterInfo();
                    chapterInfo.ID = Kit.GetGuid();
                    chapterInfo.Title = item.Html().Replace(" ", "");
                    chapterInfo.Url = "http://www.xs.la" + item.Attr("href");
                    chapterInfo.Metadata = item.Html();
                    chapterInfo.AddTime = DateTime.Now;
                    chapterInfos.Add(chapterInfo);
                }
                cif.Save(chapterInfos);

                groupBox1.Text = "初始化完成";
            }

            BookInfoData bif = new BookInfoData();


            if (bif.GetBookInfos(t => book.Equals(t)).Count <= 0)
            {
                book.AddTime = DateTime.Now;
                bif.Add(book);
            }

            Download();
        }



        private async void Download()
        {
            groupBox1.Text = "开始下载";
            ChapterInfoData cif = new ChapterInfoData(book);

            List<ChapterInfo> chapterInfos = cif.GetChapterInfos();
            progressBar1.Maximum = chapterInfos.Count;

            HttpClient client = new HttpClient();
            
            for (int i = 0; i < chapterInfos.Count; i++)
            {
                download:
                {
                    try
                    {
                        ChapterInfo item = chapterInfos[i];
                        string html = await client.GetStringAsync(item.Url);

                        Match macth = Regex.Match(html, "<script>window.location.href='(.*)';</script>");
                        if (macth.Length > 0 && macth.Groups.Count > 1)
                        {
                            html = await client.GetStringAsync(macth.Groups[1].Value);
                        }
                        groupBox1.Text = item.Title;
                        item.Content = NSoupClient.Parse(html).Select("div[id=content]").Html()
                                       .Replace(" ", "")
                                       .Replace("&nbsp;", " ")
                                       .Replace("<br>", "\r\n")
                                       .Replace("<br/>", "\r\n")
                                       .Replace("<scripttype=\"text/javascript\"src=\"/js/chaptererror.js\"></script>", "");
                        progressBar1.Value++;
                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Log.Info(httpRequestException.Message);
                        if (MessageBox.Show("下载遇到问题,是否重试","温馨提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            goto download;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            cif.Save(chapterInfos);
            this.Close();

        }

        private void FrmDownload_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show(book.Name + "添加成功", "温馨提示");
        }
    }
}
