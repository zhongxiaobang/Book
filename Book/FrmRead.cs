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
using Book.Common;
using Book.Data;

namespace Book
{
    public partial class FrmRead : Form
    {
        private BookInfo book;
        private List<ChapterInfo> chapterInfos;
        public int CurrentIndex { get; set; } = 0;
        public FrmRead(BookInfo book)
        {
            InitializeComponent();
            this.book = book;
        }

        private void FrmRead_Load(object sender, EventArgs e)
        {
            ChapterInfoData cif = new ChapterInfoData(book);
            chapterInfos = cif.GetChapterInfos();

            LoadListBox();

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                LoadContent();
                LoadTitle();
            }
           
            FrmRead_Resize(null, null);

            RecoveryConfiguration();
        }

        /// <summary>
        /// 从配置文件中恢复窗体和控件状态
        /// </summary>
        public void RecoveryConfiguration()
        {
            FrmReadConfig config = FormConfigManager.GetFrmReadConfig(book);
            if (config != null)
            {
                listBox1.Font = new Font(new FontFamily(config.ListBoxFontFamilyName), config.ListBoxFontSize);
                listBox1.ForeColor = config.ListBoxForeColor;
                listBox1.BackColor = config.ListBoxBackColor;
                textBox1.Font = new Font(new FontFamily(config.TextBoxFamilyName),config.TextBoxFontSize);
                textBox1.ForeColor = config.TextBoxForeColor;
                textBox1.BackColor = config.TextBoxBackColor;
                WindowState = config.FormWindowState;
                Left = config.FormLeft;
                Top = config.FormTop;
                Width = config.FormWidth;
                Height = config.FormHeight;

                listBox1.SelectedIndex = config.ReadIndex;
                listBox1_MouseDoubleClick(null, null);

            }
        }

        private void LoadListBox()
        {
            listBox1.Items.Clear();
            foreach (var item in chapterInfos)
            {
                listBox1.Items.Add(item.Title);
            }
        }
        public void LoadTitle()
        {
            this.Text = book.Name + "  " + chapterInfos[CurrentIndex].Title;
        }
        private void LoadContent()
        {
            textBox1.Text = chapterInfos[CurrentIndex].Content;
        }

        private void FrmRead_Resize(object sender, EventArgs e)
        {
            //listBox1.Width = Width / 5;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                CurrentIndex = listBox1.SelectedIndex;
                LoadContent();
                LoadTitle();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            int count = listBox1.Items.Count;
            if (index >= 0)
            {
                if (index == 0)
                {
                    MessageBox.Show("已经是第一章了");
                    return;
                }
                listBox1.SelectedIndex--;
                listBox1_MouseDoubleClick(null, null);
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            int count = listBox1.Items.Count;
            if (index >= 0)
            {
                if (index >= count - 1)
                {
                    MessageBox.Show("已经是最后一章了");
                    return;
                }
                listBox1.SelectedIndex++;
                listBox1_MouseDoubleClick(null, null);
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.SelectedText))
            {
                Clipboard.SetText(textBox1.SelectedText);
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = dialog.Font;
            }
        }

        private void 字体颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.ForeColor = dialog.Color;
            }
        }

        private void 背景颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = dialog.Color;
            }
        }

        private void 字体ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Font = dialog.Font;
            }
        }

        private void 字体颜色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.ForeColor = dialog.Color;
            }
        }

        private void 背景颜色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.BackColor = dialog.Color;
            }
        }

        private void FrmRead_FormClosed(object sender, FormClosedEventArgs e)
        {
            //保存窗体和控件状态
            FrmReadConfig config = new FrmReadConfig();
            config.ReadIndex = CurrentIndex;
            config.ListBoxFontFamilyName = listBox1.Font.Name;
            config.ListBoxFontSize = listBox1.Font.Size;
            config.ListBoxForeColor = listBox1.ForeColor;
            config.ListBoxBackColor = listBox1.BackColor;
            config.TextBoxFamilyName = textBox1.Font.Name;
            config.TextBoxFontSize = textBox1.Font.Size;
            config.TextBoxForeColor = textBox1.ForeColor;
            config.TextBoxBackColor = textBox1.BackColor;
            config.FormWindowState = WindowState;
            config.FormLeft = Left;
            config.FormTop = Top;
            config.FormWidth = Width;
            config.FormHeight = Height;

            FormConfigManager.SetFrmReadConfigConfig(config,book);
        }
    }
}
