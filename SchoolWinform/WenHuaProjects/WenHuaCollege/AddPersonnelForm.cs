using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WenHuaCollege.EF6;
using WenHuaCollege.Pages;

namespace WenHuaCollege
{
    public partial class AddPersonnelForm : Form
    {
        public class RoleModel
        {
            public int ID { get; set; }
            public string Name { get; set; }

            //public override string ToString()
            //{
            //    return Name;
            //}

        }

        PersonnelPage CurrentMenuPage;
        public AddPersonnelForm(PersonnelPage page)
        {
            InitializeComponent();
            CurrentMenuPage = page;

            //List<object> roles = new List<object>();
            ////RoleModel model= new RoleModel();
            ////MessageBox.Show(model.ToString());
            //roles.Add(new { ID = 1, Name = "学生" });
            //roles.Add(new { ID = 2, Name = "教师" });
            //roles.Add(new { ID = 3, Name = "超级管理员" });

            //comboBox1.DataSource = roles;
            //comboBox1.DisplayMember = "Name";   // 指定【显示的文本】取Name属性
            //comboBox1.ValueMember = "ID";       // 指定【后台取值】取ID属性

            comboBox1.DataSource = Enum.GetValues(typeof(RoleEnum));
        }

        //添加菜单数据到数据库
        private void btnSave_Click(object sender, EventArgs e)
        {
            RoleEnum selectedEnum = (RoleEnum)comboBox1.SelectedItem;
            int roleId = (int)selectedEnum;//枚举对象转int
            string text = roleId.ToString();//枚举对象转文本

            string personnelID = txtPersonnelId.Text;
            string name = txtName.Text;
            string Gender = txtGender.Text;
            string address = txtAddress.Text;

            if (string.IsNullOrEmpty(personnelID))
            {
                MessageBox.Show("请填写学号");
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请填写姓名");
                return;
            }
            if (string.IsNullOrEmpty(Gender))
            {
                MessageBox.Show("请填写性别");
                return;
            }

            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("请填写地址");
                return;
            }

            using (var db = new AppDbContext())
            {
                PersonnelTModel model = new PersonnelTModel();
                model.PersonnelID = int.Parse(personnelID);
                model.Name = name;
                model.Gender = Gender;
                model.Address = address;
                model.RoleID = roleId;

                db.Personnels.Add(model);
                db.SaveChanges();
            }
            MessageBox.Show("已添加到数据库");
            this.Close();
            CurrentMenuPage.LoadData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

