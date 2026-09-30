using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WenHuaCollege.EF6;
using WenHuaCollege.Models;
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
            HomePage homeP = new HomePage();
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

            using (var db = new AppDbContext())
            {
                //MessageBox.Show(db.Menus.ToList().Count.ToString());
                List<MenuTModel> LstMenu = db.Menus.ToList();

                foreach (MenuTModel menu in LstMenu)
                {
                    MenuUC menuUC = new MenuUC();
                    menuUC.MenuText = menu.MenuText;
                    menuUC.MenuImage = GetBitmapFromResx(menu.MenuImage);

                    menuUC.LabelClick += (newSender, nerE) =>
                    {
                        panelContent.Controls.Clear();
                        Control page = CreatePage(menu.MenuPage);
                        if (page != null)
                        {
                            page.Dock = DockStyle.Fill;
                            panelContent.Controls.Add(page);
                        }
                        else
                        {
                            MessageBox.Show("未找到对应页面：" + menu.MenuPage);
                        }
                    };
                    //把数据对象转换到数据库的数据对象
                    flPanelMenu.Controls.Add(menuUC);
                }
            }


            //根据&符号分割，获得对应的菜单数据
            //foreach (MenuTModel menu in modelT)
            //{
            //    //string[] menuInfo = menu.Split(',');

            //    MenuUC menuUC = new MenuUC();
            //    menuUC.MenuText = menu.MenuText;

            //    menuUC.MenuImage = GetBitmapFromResx(menu.MenuImage);
            //    menuUC.LabelClick += (newSender, nerE) =>
            //    {
            //        panelContent.Controls.Clear();
            //        //反射    根据读取的内容，来找到对应的类型，并且将它动态的创建对象，给到panelContent
            //        Control page = CreatePage(menu.MenuPage);
            //        if (page != null)
            //        {
            //            page.Dock = DockStyle.Fill;
            //            panelContent.Controls.Add(page);

            //        }
            //        else
            //        {
            //            MessageBox.Show("未找到对应页面");
            //        }

            //    };
            //    //string ImageName = menuInfo[1];
            //    //Image resourcesProp = (Image)Properties.Resources.ResourceManager.GetObject(ImageName);//根据名称获取图片（反射）
            //    //menuUC.MenuImage = resourcesProp;

            //    flPanelMenu.Controls.Add(menuUC);
            //    //menu.MenuText = menu
            //}

        }

        // 写在Form里面，封装方法
        private Control CreatePage(string className)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            //命名空间
            string fullType = $"WenHuaCollege.Pages.{className}";
            Type type = asm.GetType(fullType);
            if (type == null) return null;
            // 调用无参构造实例化
            object obj = Activator.CreateInstance(type);
            return obj as Control;
        }


        public Bitmap GetBitmapFromResx(string name)
        {
            Type type = typeof(Resources);
            PropertyInfo prop = type.GetProperty(name, BindingFlags.Static | BindingFlags.NonPublic);
            if (prop != null && prop.PropertyType == typeof(Bitmap))
            {
                return (Bitmap)prop.GetValue(null);
            }
            return Resources.home;
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuPage menuPage = new MenuPage();
            panelContent.Controls.Clear();
            menuPage.Dock = DockStyle.Fill;
            panelContent.Controls.Add(menuPage);

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

