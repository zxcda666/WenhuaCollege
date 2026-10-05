using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WenHuaCollege.EF6;
using WenHuaCollege.Pages;

namespace WenHuaCollege
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        //添加菜单数据到数据库
        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            string UserName = txtUserName.Text;
            string UserPassword = txtUserPassword.Text;

            if (string.IsNullOrEmpty(UserName))
            {
                MessageBox.Show("请输入用户名");
                return;
            }

            if (string.IsNullOrEmpty(UserPassword))
            {
                MessageBox.Show("请输入密码");
                return;
            }

            this.Close();
        }
    }
}

