using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenHuaCollege.EF6
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
            :base("Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=WenHuaDB;Integrated Security=True")
        {

        }

        public DbSet<MenuTModel> Menus { get; set; } 
    }
}
