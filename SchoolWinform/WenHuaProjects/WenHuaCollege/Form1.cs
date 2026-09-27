using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WenHuaCollege.Pages;
using WenHuaCollege.Properties;
using WenHuaCollege.UserControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WenHuaCollege
{
    public delegate void MyDelegate(string msg); //委托
    public partial class Form1 : Form
    {
        public event MyDelegate MyEvent; //基于委托的时间
        public Form1()
        {
            InitializeComponent();


            //MyDelegate myDelegate = ShowMessage;
            //myDelegate("这是一条弹窗信息");
        }

        

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void menuUC2_LabelClick_1(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            Homepage homeP = new Homepage();
            panelContent.Controls.Add(homeP);
        }

        private void menuUC1_LabelClick(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            SettingPage setP = new SettingPage();
            panelContent.Controls.Add(setP); 
        }

        private void menuUC2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //menuUC1.Visible = false;
            //menuUC2.Visible = false;
            //menuUC3.Visible = false;
            //menuUC4.Visible = false;

            string path = @"C:\Users\Admin（无密码）\Desktop\AllMenus.txt"; //文件路径
            string menuID = File.ReadAllText(path);
            string[] ids = menuID.Split(',');
             

`   `            //读取整个文件内容
            string menuContent = File.ReadAllText(path);

hbhgvb           
            //根据&符号分割，获得对应的菜单数据
            string[] menus = menuContent.Split('&');
            foreach(string menu in menus)
            {
                string[] menuInfo = menu.Split(',');

                MenuUC menuUC = new MenuUC();
                menuUC.MenuText = menuInfo[0];
                string ImageName = menuInfo[1];
                Image resourcesProp = (Image)Properties.Resources.ResourceManager.GetObject(ImageName);//根据名称获取图片（反=9876`  ）


                menuUC.MenuImage = resourcesProp;
                flPanelMenu.Controls.Add(menuUC);
                //menu.MenuText = menu
            }
            
            //foreach(string item in ids)
            //{
            //    if(item=="1")
            //    {
            //        //menuUC1.Visible = true;
            //        MenuUC menu1 = new MenuUC();
            //        menu1.MenuText = "首页";
            //        menu1.MenuImage = Resources.home;
            //        flPanelMenu.Controls.Add(menu1);
            //    }
            //    else if (item == "2")
            //    {
            //        //menuUC2.Visible = true;
            //        MenuUC menu2 = new MenuUC();
            //        menu2.MenuImage = Resources.setting;
            //        menu2.MenuText = "设置";
            //        flPanelMenu.Controls.Add(menu2);
            //    }
            //    else if (item == "3")
            //    {

            //        //menuUC3.Visible = true;
            //        MenuUC menu3 = new MenuUC();
            //        menu3.MenuImage = Resources.selection;
            //        menu3.MenuText = "选课";
            //        flPanelMenu.Controls.Add(menu3);
            //    }
            //    else if (item == "4")
            //    {
            //        //menuUC4.Visible = true;
            //        MenuUC menu4 = new MenuUC();
            //        menu4.MenuImage = Resources.about;
            //        menu4.MenuText = "关于";
            //        flPanelMenu.Controls.Add(menu4);
            //    }
            //}

            ////提取数据里面所有的菜单
            //if (menuID == "1")
            //{
            //    menuUC1.Visible = true;

            //}
            //else if(menuID == "2")
            //{
            //    menuUC2.Visible = true;
            //}

        }
    }
}
