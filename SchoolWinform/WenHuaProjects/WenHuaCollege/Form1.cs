using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            MyEvent += ShowMessage;
            MyEvent("这是一条弹窗信息");

            ShowMessage("这是一条弹窗信息!");
        }

        

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
