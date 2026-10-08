using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenHuaCollege.EF6
{
    [Table("Personnel")]
    public class PersonnelTModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelID { get; set; }
        //姓名
        public string Name { get; set; }

        public string UesrPassword { get; set; }
        //性别
        public string Gender { get; set; }
        //家庭住址 
        public string Address { get; set; }


        public int RoleID { get; set; }//角色ID

        [NotMapped]
        //public string RoleName
        //{
        //    get
        //    {
        //        string name = "";
        //        if (RoleID == 1)
        //            name = "学生";
        //        else if (RoleID == 2)
        //            name = "教师";
        //        else if (RoleID == 3)
        //            name = "超级管理员";
        //        else
        //            name = "未知角色";
        //        return name;
        //    }
        //}
        public string RoleName
        {
            get
            {
                if (!Enum.IsDefined(typeof(RoleEnum), RoleID))
                    return "未知角色";
                RoleEnum role = (RoleEnum)RoleID;
                return role.ToString();
            }
        }
    }
}