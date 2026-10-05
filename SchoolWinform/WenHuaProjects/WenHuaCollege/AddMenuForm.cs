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
    public partial class AddMenuForm : Form
    {
        MenuPage CurrentMenuPage;
        public AddMenuForm(MenuPage page)
        {
            InitializeComponent();
            CurrentMenuPage = page;
        }

        //添加菜单数据到数据库
        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            string menuText = txtMenuText.Text;
            string menuImage = txtMenuImage.Text;
            string menuPage = txtMenuPage.Text;

            if (string.IsNullOrEmpty(menuText))
            {
                MessageBox.Show("请填写菜单名称");
                return;
            }

            if (string.IsNullOrEmpty(menuImage))
            {
                MessageBox.Show("请填写菜单图片");
                return;
            }
            if (string.IsNullOrEmpty(menuPage))
            {
                MessageBox.Show("请填写菜单页面");
                return;
            }

            using(var db=new AppDbContext())
            {
                MenuTModel model = new MenuTModel();
                model.MenuText = menuText;
                model.MenuImage = menuImage;
                model.MenuPage = menuPage;

                db.Menus.Add(model);
                db.SaveChanges();
            }
            MessageBox.Show("已添加到数据库");
            this.Close();
            CurrentMenuPage.LoadMenu();
        }
    }
}

