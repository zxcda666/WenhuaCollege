using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenHuaCollege.EF6;

namespace WenHuaCollege
{
    public class GlobalClass
    {
        private static GlobalClass _instance = null;

        public static GlobalClass Instance()
        {
            if (_instance == null)
            {
                _instance = new GlobalClass();
            }
            return _instance;
        }

        //当前登录的用户信息
        public PersonnelTModel CurrentLoginUser { get; set; }

        //当前登录用户所属的角色权限信息
        public List<PermissionsTModel> UserPermissions { get; set; }

        public Form1 CurrentIndexForm { get; set; }
    }
}
