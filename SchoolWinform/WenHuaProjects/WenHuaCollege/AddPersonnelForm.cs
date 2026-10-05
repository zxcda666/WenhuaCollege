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
    public partial class AddPersonnelForm : Form
    {
        PersonnelPage CurrentMenuPage;
        public AddPersonnelForm(PersonnelPage page)
        {
            InitializeComponent();
            CurrentMenuPage = page;
        }

        //添加菜单数据到数据库
        private void btnSave_Click(object sender, EventArgs e)
        {
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

            using (var db=new AppDbContext())
            {
                PersonnelTModel model = new PersonnelTModel();
                model.PersonnelID = int.Parse(personnelID);
                model.Name = name;
                model.Gender = Gender;
                model.Address = address;

                db.Personnels.Add(model);
                db.SaveChanges(); 
            }
            MessageBox.Show("已添加到数据库");
            this.Close();
            CurrentMenuPage.LoadData();
        }
    }
}

