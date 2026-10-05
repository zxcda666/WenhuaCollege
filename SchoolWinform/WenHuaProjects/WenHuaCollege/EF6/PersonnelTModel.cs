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

    }

}