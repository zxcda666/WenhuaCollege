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
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();

            //查询数据，展示到界面中

            //美化
            BeautifyDataGridView(dataGridView1);
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
            AddMenuForm addMenuForm = new AddMenuForm();
            addMenuForm.ShowDialog();
        }
    }
}
