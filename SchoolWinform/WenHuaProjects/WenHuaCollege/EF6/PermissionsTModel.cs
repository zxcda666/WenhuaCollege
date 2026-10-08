using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenHuaCollege.EF6
{
    [Table("PermissionT")]
    public class PermissionsTModel
    {
        public int Id { get; set; }
        //角色ID
        public int RoleID { get; set; }//角色ID

        //菜单
        public string MenuPage { get; set; }

        //菜单功能
        //删除，查看，Excel
        public string Functions { get; set; }
    }
}
