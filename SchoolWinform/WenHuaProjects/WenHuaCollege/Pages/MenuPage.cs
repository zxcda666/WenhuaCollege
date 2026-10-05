using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WenHuaCollege;
using WenHuaCollege.EF6;
using WenHuaCollege.Properties;

namespace WenHuaCollege.Pages
{
    public partial class MenuPage : UserControl
    {
        Form1 currentForm1;
        public MenuPage(Form1 form1)
        {
            InitializeComponent();

            //查询数据，展示到界面中

            //美化
            BeautifyDataGridView(dataGridView1);
            currentForm1 = form1;
        }

        private void MenuPage_Load(object sender, EventArgs e)
        {
            using (var db = new AppDbContext())
            {
                List<MenuTModel> menus = db.Menus.ToList();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                // 关闭多行选择，只能单选一行
                dataGridView1.MultiSelect = false;
                dataGridView1.ColumnHeadersHeight = 80;//设置标题高度80

                dataGridView1.DataSource = menus; 
            }
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
            AddMenuForm addMenuForm = new AddMenuForm(this);
            addMenuForm.ShowDialog();
        }

        public void LoadMenu()
        {
            using (var db = new AppDbContext())
            {
                List<MenuTModel> menus = db.Menus.ToList();
                dataGridView1.DataSource = menus;
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
            label5.Text = startLeft.ToString();
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

            string Id = dataGridView1.Rows[e.RowIndex].Cells["ColId"].Value.ToString();

            if (e.ColumnIndex == dataGridView1.Columns["ColOperation"].Index && e.RowIndex >= 0)
            {
                //编辑图片的区域
                if (xInCell > startLeft && xInCell < startLeft + imgWidth)
                {
                    //MessageBox.Show($"你点击了编辑图片的位置{Id}");
                    //创建一个编辑菜单的窗体实例，然后传入对应的对象
                    EditMenuForm editMenuForm = new EditMenuForm(this,Id);
                    editMenuForm.ShowDialog();
                    return;
                }
                //删除图片的区域
                else if (xInCell > startLeft + imgWidth + spacing && xInCell < startLeft + imgWidth + spacing + imgWidth)
                {
                    //MessageBox.Show($"你点击了删除图片的位置{Id}");
                    DialogResult result = MessageBox.Show("是否需要删除这行数据","删除警告提醒",MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                        return;

                    //删除数据
                    using (var db = new AppDbContext())
                    {
                        MenuTModel model = new MenuTModel();
                        model.Id = int.Parse(Id);

                        db.Menus.Attach(model);
                        db.Menus.Remove(model);
                        db.SaveChanges();
                    }
                    LoadMenu();
                    return;
                }
            }
            //if (yInCell > position.Y-rect.Y)
            //{
            //    dataGridView1.Cursor = Cursors.Default;
            //}


        }
    }
}