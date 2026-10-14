using Org.BouncyCastle.Asn1.Mozilla;
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
    public partial class MenuFunctionPage : UserControl
    {
        public MenuTModel CurrentMenuModel { get; set; }
        public MenuFunctionPage(MenuTModel model)
        {
            InitializeComponent();

            CurrentMenuModel = model;

            labMenuText.Text = model.MenuText;

            foreach (MenuFunctionEnum item in Enum.GetValues(typeof(MenuFunctionEnum)))
            {
                CheckBox check = new CheckBox();
                check.Text = item.ToString();

                flowLayoutPanel1.Controls.Add(check);
            }
        }
        //返回string和List<string>
        public Tuple<string, List<string>> GetMenuPageAndFunctions()
        {
            //获取当前控件展示的菜单信息
            string menuPage = CurrentMenuModel.MenuPage;

            List<string> functions = new List<string>();

            //获取当前控件中选中的CheckBox内容
            foreach (var item in flowLayoutPanel1.Controls)
            {
                //把选中的 CheckBox 的 Text 文本放进`functions`列表
                CheckBox check = (CheckBox)item;
                if (check.Checked)
                {
                    string text = check.Text;
                    functions.Add(text);
                }
            }

            return Tuple.Create(menuPage, functions);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void RestoreOptions(List<PermissionsTModel> models)
        {
            foreach (var itemCheck in flowLayoutPanel1.Controls)
            {
                CheckBox check = (CheckBox)itemCheck;
                check.Checked = false;
            }
            //当前所选角色的所有数据
            foreach (var item in models)
            {
                if (item.MenuPage == CurrentMenuModel.MenuPage)
                {
                    //我就读取所有checkbox,看看你传递过来的权限功能是哪个，匹配上的话，就直接勾选上
                    foreach (var itemCheck in flowLayoutPanel1.Controls)
                    {
                        CheckBox check = (CheckBox)itemCheck;
                        if (item.Functions.Contains(check.Text))
                            check.Checked = true;
                    }
                }
            }
        }
    }
}
