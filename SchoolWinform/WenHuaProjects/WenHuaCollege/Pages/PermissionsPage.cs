using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WenHuaCollege.EF6;

namespace WenHuaCollege.Pages
{
    public partial class PermissionsPage : UserControl
    {
        public PermissionsPage()
        {
            InitializeComponent();
            //绑定角色选择
            comboBox1.DataSource = Enum.GetValues(typeof(RoleEnum));

            //查询所有的菜单数据，并且根据菜单名称生成对应的菜单功能
            using (var db = new AppDbContext())
            {
                List<MenuTModel> models = db.Menus.ToList();
                foreach (var model in models)
                {
                    MenuFunctionPage menuFunctionPage = new MenuFunctionPage(model);
                    menuFunctionPage.Dock = DockStyle.Top;

                    panel2.Controls.Add(menuFunctionPage);
                }
            }
            ////获取选择角色信息，然后查询对应角色的权限信息，并且设置到对应的菜单功能页面中
            //RoleEnum roleEnum = (RoleEnum)comboBox1.SelectedItem;
            //int roleID = (int)roleEnum;
            //List<PermissionsTModel> lstPermissions = new List<PermissionsTModel>();
            //using(var db = new AppDbContext())
            //{
            //    lstPermissions = db.permissions.Where(m => m.RoleID == roleID).ToList();//取到对应角色信息的数据
            //}

            //foreach(var item in panel2.Controls)
            //{
            //    MenuFunctionPage page = (MenuFunctionPage)item;

            //    page.RestoreOptions(lstPermissions);
            //}
            updataValue();
        }

        public void updataValue()
        {
            //获取选择角色信息，然后查询对应角色的权限信息，并且设置到对应的菜单功能页面中
            RoleEnum roleEnum = (RoleEnum)comboBox1.SelectedItem;
            int roleID = (int)roleEnum;
            List<PermissionsTModel> lstPermissions = new List<PermissionsTModel>();
            using (var db = new AppDbContext())
            {
                lstPermissions = db.permissions.Where(m => m.RoleID == roleID).ToList();//取到对应角色信息的数据
            }

            foreach (var item in panel2.Controls)
            {
                MenuFunctionPage page = (MenuFunctionPage)item;

                page.RestoreOptions(lstPermissions);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //获取角色ID
            RoleEnum roleEnum = (RoleEnum)comboBox1.SelectedItem;
            int roleID = (int)roleEnum;

            List<PermissionsTModel> models = new List<PermissionsTModel>();
            foreach (var item in panel2.Controls)
            {
                //获取每个菜单功能页面的权限设置
                MenuFunctionPage functionPage = (MenuFunctionPage)item;
                Tuple<string, List<string>> result = functionPage.GetMenuPageAndFunctions();

                PermissionsTModel model = new PermissionsTModel
                {
                    RoleID = roleID,
                    MenuPage = result.Item1,
                    Functions = string.Join(",", result.Item2)
                };
                models.Add(model);
            }

            using(var db = new AppDbContext())
            {
                               //删除当前角色的所有权限
                var existingPermissions = db.permissions.Where(p => p.RoleID == roleID).ToList();
                db.permissions.RemoveRange(existingPermissions);
                //添加新的权限
                db.permissions.AddRange(models);
                db.SaveChanges();
            }
            MessageBox.Show("权限保存成功！");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updataValue();
        }
    }
        
}
