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
    public partial class EditMenuForm : Form
    {
        MenuPage CurrentMenuPage;
        public EditMenuForm(MenuPage page,String id)
        {
            InitializeComponent();
            int menuID = int.Parse(id);
            CurrentMenuPage = page;
            //通过id去查询数据库的菜单数据，直接绑定界面文本框中
            using (var DB = new AppDbContext())
            {
                MenuTModel model = DB.Menus.FirstOrDefault(e => e.Id == menuID);
                this.txtMenuText.Text = model.MenuText;
                this.txtMenuImage.Text = model.MenuImage;
                this.txtMenuPage.Text = model.MenuPage;
                label5.Text = model.Id.ToString();
            }
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
            int menuID = int.Parse(label5.Text);
            //获取ID数据，继续查询数据，然后讲编辑后的数据进行修改和保存
            using (var db = new AppDbContext())
            {
                MenuTModel model = db.Menus.FirstOrDefault(m => m.Id == menuID); 
                model.MenuText = menuText;
                model.MenuImage = menuImage;
                model.MenuPage = menuPage;
                db.SaveChanges();
            }
            CurrentMenuPage.LoadMenu();

            MessageBox.Show("数据保存完成！");

            this.Close();

            return;
        }
    }
}

