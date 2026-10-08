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
        public MenuFunctionPage(MenuTModel model)
        {
            InitializeComponent();

            labMenuText.Text = model.MenuText;

            foreach(var item in Enum.GetValues(typeof(MenuFunctionEnum)))
            {
                CheckBox check = new CheckBox();
                check.Text = item.ToString();

                flowLayoutPanel1.Controls.Add(check);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
