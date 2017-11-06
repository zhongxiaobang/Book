using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Book
{
    public class FrmReadConfig
    {
        /// <summary>
        /// 阅读章节索引
        /// </summary>
        public int ReadIndex { get; set; }
        /// <summary>
        /// 字体名称（ListBox）
        /// </summary>
        public string ListBoxFontFamilyName { get; set; }
        /// <summary>
        /// 字体大小（ListBox）
        /// </summary>
        public float ListBoxFontSize { get; set; }
        /// <summary>
        /// 字体颜色（ListBox）
        /// </summary>
        public Color ListBoxForeColor { get; set; }
        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color ListBoxBackColor { get; set; }
        /// <summary>
        /// 字体名称（TextBox）
        /// </summary>
        public string TextBoxFamilyName { get; set; }
        /// <summary>
        /// 字体大小（TextBox）
        /// </summary>
        public float TextBoxFontSize { get; set; }
        /// <summary>
        /// 前景颜色（TextBox）
        /// </summary>
        public Color TextBoxForeColor { get; set; }
        /// <summary>
        /// 背景颜色（TextBox）
        /// </summary>
        public Color TextBoxBackColor { get; set; }
        /// <summary>
        /// 窗体状态
        /// </summary>
        public FormWindowState FormWindowState { get; set; }
        /// <summary>
        /// 窗体左边
        /// </summary>
        public int FormLeft { get; set; }
        /// <summary>
        /// 窗体右边
        /// </summary>
        public int FormTop { get; set; }
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public int FormWidth { get; set; }
        /// <summary>
        /// 窗体高度
        /// </summary>
        public int FormHeight { get; set; }
    }
}
