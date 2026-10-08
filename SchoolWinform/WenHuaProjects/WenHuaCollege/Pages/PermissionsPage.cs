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
        }
    }
}
