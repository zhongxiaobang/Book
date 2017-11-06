using Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Common
{
    public class UserInfoHelper
    {
        public static UserInfo GetUserInfo()
        {
            return Kit.Session["UserInfo"] as UserInfo;
        }
    }
}
