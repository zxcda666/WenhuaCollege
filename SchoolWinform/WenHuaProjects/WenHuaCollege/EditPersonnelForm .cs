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
    public partial class EditPersonnelForm : Form
    {
        PersonnelPage CurrentPersonnelPage;
        int CurrentPersonnelID { get; set; }

        public EditPersonnelForm(PersonnelPage page,String id)
        {
            InitializeComponent();
            CurrentPersonnelID = int.Parse(id);

            CurrentPersonnelPage = page;
            //通过id去查询数据库的菜单数据，直接绑定界面文本框中
            using (var DB = new AppDbContext())
            {
                PersonnelTModel model = DB.Personnels.FirstOrDefault(e => e.PersonnelID == CurrentPersonnelID);
                this.txtPersonnelId.Text = model.PersonnelID.ToString();
                this.txtName.Text = model.Name;
                this.txtGender.Text = model.Gender;
                this.txtAddress.Text = model.Address;
                label5.Text = model.PersonnelID.ToString();
            }
        }

        //添加菜单数据到数据库
        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            string personnelId = txtPersonnelId.Text;
            string name = txtName.Text;
            string gender = txtGender.Text;
            string address = txtAddress.Text;

            if (string.IsNullOrEmpty(personnelId))
            {
                MessageBox.Show("请填写人员ID");
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请填写人员姓名");
                return;
            }
            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("请填写性别");
                return;
            }
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("请填写地址");
                return;
            }

            //获取ID数据，继续查询数据，然后讲编辑后的数据进行修改和保存
            using (var db = new AppDbContext())
            {
                PersonnelTModel model = db.Personnels.FirstOrDefault(m => m.PersonnelID == CurrentPersonnelID); 
                model.Name = name;
                model.Gender = gender;
                model.Address = address;
                db.SaveChanges();
            }
            CurrentPersonnelPage.LoadData();

            MessageBox.Show("数据保存完成！");

            this.Close();

            return;
        }
    }
}

