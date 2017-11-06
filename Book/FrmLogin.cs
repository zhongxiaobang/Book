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
using Book.Model;

namespace Book
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegistration fr = new FrmRegistration();
            fr.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("暂不支持", "温馨提示");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserInfoData userData = new UserInfoData();

            UserInfo userInfo = userData.GetUserInfos()
                                .Where(t => t.UserName == textBox1.Text && t.Password == Security.ParseMd5(textBox2.Text))
                                .FirstOrDefault();
            if (userInfo == null)
            {
                MessageBox.Show("用户名或者密码错误", "温馨提示");
            }
            else
            {
                userInfo.LoginTime = DateTime.Now;

                Kit.Session["UserInfo"] = userInfo;

                FrmMain frmMain = new FrmMain();
                frmMain.Show();

                this.Hide();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
