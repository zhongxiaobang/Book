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
    public partial class FrmRegistration : Form
    {
        public FrmRegistration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserInfoData userInfoData = new UserInfoData();

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }

            if (textBox1.Text == "admin")
            {
                MessageBox.Show("不能使用admin作为用户名");
                return;
            }
            if (userInfoData.GetUserInfos(t => t.UserName == textBox1.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("该用户名已存在");
                return;
            }

            UserInfo userInfo = new UserInfo();
            userInfo.ID = Kit.GetGuid();
            userInfo.UserName = textBox1.Text;
            userInfo.Password = Security.ParseMd5(textBox2.Text);
            userInfo.NickName = textBox3.Text;
            userInfo.RegistrationTime = DateTime.Now;
            userInfoData.Add(userInfo);
            MessageBox.Show("注册成功");
            this.Close();
        }

    }
}
