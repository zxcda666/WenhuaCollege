using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
using WenHuaCollege;
using WenHuaCollege.EF6;
using WenHuaCollege.Properties;

namespace WenHuaCollege.Pages
{
    public partial class PersonnelPage : UserControl
    {
        Form1 currentForm1;
        public PersonnelPage()
        {
            InitializeComponent();
        }
        public PersonnelPage(Form1 form1)
        {
            InitializeComponent();

            //查询数据，展示到界面中

            //美化
            BeautifyDataGridView(dataGridView1);
            currentForm1 = form1;
        }

        private void MenuPage_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void BeautifyDataGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.RowHeadersVisible = false;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 87, 162);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 40; // 可根据需要调整高度

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("微软雅黑", 10);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowTemplate.Height = 30;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgv.GridColor = Color.LightGray;

            //单元格内容居中
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.SortMode = DataGridViewColumnSortMode.NotSortable;//列标题右边有预留一个排序小箭头的位置，所以整个列标题就向左边多一点，而当把SortMode属性设置为NotSortable时，不使用排序，也就没有那个预留的位置，所有完全居中了
            }

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //打开添加菜单的页面
        private void btnAddMenu_Click(object sender, EventArgs e)
        {

            //AddMenuForm addMenuForm = new AddMenuForm(this);
            //addMenuForm.ShowDialog();
            AddPersonnelForm addPersonnelForm = new AddPersonnelForm(this);
            addPersonnelForm.ShowDialog();
        }

        public void LoadData()
        {
            using (var db = new AppDbContext())
            {
                List<PersonnelTModel> personnels = db.Personnels.ToList();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                // 关闭多行选择，只能单选一行
                dataGridView1.MultiSelect = false;
                dataGridView1.ColumnHeadersHeight = 80;//设置标题高度80

                dataGridView1.DataSource = personnels;
            }

            //currentForm1.LoadForm();
        }

        

        private void dataGridView1_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["ColOperation"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                int imgWidth = 22;
                int imgHeight = 22;
                int spacing = 5;

                int startLeft = e.CellBounds.Left + (e.CellBounds.Width - (imgWidth * 2 + spacing))/2;
                int startTop = e.CellBounds.Top + (e.CellBounds.Height - imgHeight) / 2;

                e.Graphics.DrawImage(Resources.edit, new Rectangle(startLeft, startTop, imgWidth, imgHeight));
                e.Graphics.DrawImage(Resources.delete, new Rectangle(startLeft + imgWidth + spacing, startTop, imgWidth, imgHeight));
                e.Handled = true;
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            label1.Text = $"{e.RowIndex},{e.ColumnIndex}";

            //先获取到鼠标的位置
            Point position = dataGridView1.PointToClient(Cursor.Position);//当前page页面里的相对位置
            label2.Text = $"{position.X},{position.Y}";

            //现获取当前单元格的信息
            Rectangle rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex,e.RowIndex,false);
            label3.Text = $"{rect.X},{rect.Y},{rect.Width},{rect.Height}";

            int xInCell = position.X - rect.X;
            int yInCell = position.Y - rect.Y;
            label4.Text = yInCell.ToString();//格子里的相对位置

            //计算当前图片在单元格的left
            int imgWidth = 22;
            int spacing = 5;

            int startLeft = (rect.Width - (imgWidth * 2 + spacing)) / 2;
            if (e.ColumnIndex == dataGridView1.Columns["ColOperation"].Index && e.RowIndex >= 0)
            {
                if (xInCell > startLeft && xInCell < startLeft + imgWidth)
                {
                    dataGridView1.Cursor = Cursors.Hand;
                }
                else if (xInCell > startLeft + imgWidth + spacing && xInCell < startLeft + imgWidth + spacing + imgWidth)
                {
                    dataGridView1.Cursor = Cursors.Hand;
                }
                else
                {
                    dataGridView1.Cursor = Cursors.Default;
                }
            }

            if (yInCell > imgWidth)
            {
                dataGridView1.Cursor = Cursors.Default;
            }

            //if (yInCell > position.Y-rect.Y)
            //{
            //    dataGridView1.Cursor = Cursors.Default;
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //先获取到鼠标的位置
            Point position = dataGridView1.PointToClient(Cursor.Position);//当前page页面里的相对位置

            //现获取当前单元格的信息
            Rectangle rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            int xInCell = position.X - rect.X;
            int yInCell = position.Y - rect.Y;


            //计算当前图片在单元格的left
            int imgWidth = 22;
            int spacing = 5;

            int startLeft = (rect.Width - (imgWidth * 2 + spacing)) / 2;

            string Id = dataGridView1.Rows[e.RowIndex].Cells["ColPersonnelID"].Value.ToString();

            if (e.ColumnIndex == dataGridView1.Columns["ColOperation"].Index && e.RowIndex >= 0)
            {
                //编辑图片的区域
                if (xInCell > startLeft && xInCell < startLeft + imgWidth)
                {
                    //MessageBox.Show($"你点击了编辑图片的位置{Id}");
                    //创建一个编辑菜单的窗体实例，然后传入对应的对象
                    EditPersonnelForm editPersonnelForm = new EditPersonnelForm(this, Id);
                    editPersonnelForm.ShowDialog();
                    return;
                }
                //删除图片的区域
                else if (xInCell > startLeft + imgWidth + spacing && xInCell < startLeft + imgWidth + spacing + imgWidth)
                {
                    //MessageBox.Show($"你点击了删除图片的位置{Id}");
                    DialogResult result = MessageBox.Show("是否需要删除这行数据", "删除警告提醒", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                        return;

                    //删除数据
                    using (var db = new AppDbContext())
                    {
                        PersonnelTModel model = new PersonnelTModel();
                        model.PersonnelID = int.Parse(Id);

                        db.Personnels.Attach(model);
                        db.Personnels.Remove(model);
                        db.SaveChanges();
                    }
                    LoadData();
                    return;
                }
            }
            //if (yInCell > position.Y-rect.Y)
            //{
            //    dataGridView1.Cursor = Cursors.Default;
            //}

        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            List<PersonnelTModel> lst = ReadExcelToPersonnels("C:\\Users\\Admin（无密码）\\Desktop\\Personnel数据.xlsx");
            //把导入的数据同步到数据库
            using (var db = new AppDbContext())
            {
                //查询当前数据库最大的PersonnelID数据
                PersonnelTModel model = db.Personnels.OrderByDescending(m => m.PersonnelID).FirstOrDefault();//查到学号最大的数据
                int maxPersonnelID = model.PersonnelID;
                foreach(var item in lst)
                {
                    maxPersonnelID += 1;//最大学号后面加一模拟学号
                    item.PersonnelID = maxPersonnelID;
                }
                //Add添加单个模型数据，AddRange添加集合数据
                db.Personnels.AddRange(lst);
                db.SaveChanges();
            }

            LoadData();
        }

        /// <summary>
        /// 读取Excel表格，返回List<PersonnelTModel>模型数据
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns></returns>
        public static List<PersonnelTModel> ReadExcelToPersonnels(string filePath)
        {
            List<PersonnelTModel> list = new List<PersonnelTModel>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                //读取工作簿对象 .xlsx（Excel 2007+，XSSF） .xls（Excel 2003，HSSF）
                //IWorkbook workbook = new HSSFWorkbook(); // 2003
                IWorkbook workbook = new XSSFWorkbook(fs);
                //读取工作簿对象下的Sheet页面对象
                ISheet sheet = workbook.GetSheetAt(0);

                //循环遍历所有行数据（直接跳过第一行）
                for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    if (i == 0)
                        continue;

                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    PersonnelTModel model = new PersonnelTModel
                    {
                        Name = row.GetCell(0).ToString(),
                        Gender = row.GetCell(1).ToString(),
                        Address = row.GetCell(2).ToString()
                    };

                    list.Add(model);
                }
            }
            return list;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //获取数据库的数据
            using(var db= new AppDbContext())
            {
                List<PersonnelTModel> list = db.Personnels.ToList();

                ExportPersonnelToExcel(list, "C:\\Users\\Admin（无密码）\\Desktop\\Personnel_202610042356.xlsx");
            }
        }

        /// <summary>
        /// 导出List<PersonnelTModel>模型数据到Excel
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void ExportPersonnelToExcel(List<PersonnelTModel> list, string filePath)
        {
            //创建xlsx格式的工作簿
            IWorkbook workbook = new XSSFWorkbook(); // xlsx
            ISheet sheet = workbook.CreateSheet("Personnel");

            //创建表头
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("学号");
            headerRow.CreateCell(1).SetCellValue("姓名");
            headerRow.CreateCell(2).SetCellValue("性别");
            headerRow.CreateCell(3).SetCellValue("家庭住址");

            //已有表头，直接从第二行开始创建
            int i = 1;
            foreach (var model in list)
            {
                IRow row = sheet.CreateRow(i);

                row.CreateCell(0).SetCellValue(model.PersonnelID);
                row.CreateCell(1).SetCellValue(model.Name);
                row.CreateCell(2).SetCellValue(model.Gender);
                row.CreateCell(3).SetCellValue(model.Address);

                //每次创建一行，i++，下次创建下一行
                i++;
            }

            //写入文件
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fs);
            }
        }
    }
}