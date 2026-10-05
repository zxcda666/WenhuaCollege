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
        //static AppDbContext()
        //{
        //    //数据库已存在且手工维护，关闭 EF6 的模型一致性校验，避免 “model has changed” 异常
        //    Database.SetInitializer<AppDbContext>(null);
        //}

        public AppDbContext()
            :base("Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=WenHuaDB;Integrated Security=True")
        {

        }

        public DbSet<MenuTModel> Menus { get; set; } 

        public DbSet<PersonnelTModel> Personnels { get; set; }
    }
}
