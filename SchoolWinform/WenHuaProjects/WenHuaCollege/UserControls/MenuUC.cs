using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WenHuaCollege.UserControls
{
    public partial class MenuUC : UserControl
    {
        [Browsable(true)]
        public event EventHandler LabelClick;
        public MenuUC()
        {
            InitializeComponent();
            
            foreach (Control item in this.Controls)//遍历所有控件
            {
                //给所有的用户控件添加点击事件
                item.Click += AllControlsClick;
                //添加当前菜单用户控件的鼠标移入事件
                item.MouseEnter += MenuUC_MouseEnter;
                //添加当前菜单用户控件的鼠标移出事件
                item.MouseLeave += MenuUc_MouseLeave;

                //添加当前菜单用户控件的鼠标按下事件
                item.MouseDown += MenuUc_MouseDown;

                item.MouseUp += MenuUc_MouseUp;
            }
           
            this.MouseEnter += MenuUC_MouseEnter;
            //添加当前菜单用户控件的鼠标移出事件
            this.MouseLeave += MenuUc_MouseLeave;

            this.MouseDown += MenuUc_MouseDown;

            this.MouseUp += MenuUc_MouseUp;
        }

        private void MenuUc_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackColor = MenuBaseColor;
        }

        private void MenuUc_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackColor = MenuPressColor;
        }

        private void MenuUc_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = MenuBaseColor;
        }

        private void MenuUC_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = MenuHoverColoer;//移入变为此颜色
        }

        public void AllControlsClick(object sender,EventArgs e)
        {
            LabelClick?.Invoke(sender, e);
        }

        //特性
        [Browsable(true)]
        [Description("这是设置当前菜单名称的属性")]
        //属性
        public string MenuText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Browsable(true)]
        [Description("这是设置当前菜单名称的属性")]
        public Image MenuImage
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }



        [Description("控件基本颜色")]
        [DefaultValue(typeof(Color),"35,40,45")]
        public Color MenuBaseColor { get; set; } = Color.FromArgb(30,40,45);

        [Description("鼠标移入控件的颜色")]
        [DefaultValue(typeof(Color), "55,60,70")]
        public Color MenuHoverColoer { get; set; } = Color.FromArgb(55,60,70);


        [Description("鼠标点击控件的颜色")]
        public Color MenuPressColor { get; set; } = Color.Red;

        private void MenuUC_Click(object sender, EventArgs e)
        {
            LabelClick?.Invoke(sender, e);
        }
    }
}
