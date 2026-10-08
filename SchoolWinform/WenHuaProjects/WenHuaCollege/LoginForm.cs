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
        private void btnSave_Click(object sender, EventArgs e)
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

            using(var DB = new AppDbContext())
            {
                PersonnelTModel model = DB.Personnels.Where(m=>m.Name==UserName&&m.UesrPassword==UserPassword).FirstOrDefault();
                if(model == null)
                {
                    MessageBox.Show("输入的用户名或密码不正确！");
                    return;
                }
                GlobalClass.Instance().CurrentLogin = model;
            }

            this.DialogResult = DialogResult.OK; 
            this.Close();
        }
    }
}

