using Book.Common;
using Book.Data;
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

namespace Book
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            int width = listView1.Width / 3;
            foreach (var item in listView1.Columns)
            {
                ((ColumnHeader)item).Width = width;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            listView1_Resize(listView1, e);
            LoadListViewData();
        }

        /// <summary>
        /// 加载listView数据
        /// </summary>
        public void LoadListViewData()
        {
            listView1.Items.Clear();

            UserBookListData ubld = new UserBookListData();

            UserInfo userInfo = UserInfoHelper.GetUserInfo();

            List<BookInfo> bookInfo = ubld.GetBooks(userInfo.ID);

            foreach (var item in bookInfo)
            {
                ListViewItem viewItem = new ListViewItem(item.Name);
                viewItem.SubItems.Add(item.Loeva);
                viewItem.SubItems.Add(item.UpdateTime.ToString("yyyy-MM-dd"));
                viewItem.Tag = item;
                listView1.Items.Add(viewItem);

            }
        }

        private void 搜索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSearch frmSerach = new FrmSearch();
            frmSerach.frmMain = this;
            frmSerach.Show();
        }

        private void 阅读ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                BookInfo book = listView1.SelectedItems[0].Tag as BookInfo;
                FrmRead fr = new FrmRead(book);
                fr.Show();
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

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            阅读ToolStripMenuItem_Click(sender, null);
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadListViewData();
        }
    }
}
