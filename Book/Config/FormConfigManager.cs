using Book.Common;
using Book.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    public class FormConfigManager
    {
        private static readonly UserInfo userInfo = UserInfoHelper.GetUserInfo();

        public static FrmReadConfig GetFrmReadConfig(BookInfo book)
        {
            string path = "data/" + userInfo.UserName + "/config/" + book.Name + "-" + book.Author + "/FrmReadConfig.json";
            if (File.Exists(path))
            {
                string json = FileUtils.ReadAllText(path, Encoding.UTF8);
                return Kit.ToObject<FrmReadConfig>(json);
            }
            return null;
        }

        public static void SetFrmReadConfigConfig(FrmReadConfig config,BookInfo book)
        {
            string json = Kit.ToJson(config,true);
            FileUtils.WriteAllText("data/" + userInfo.UserName + "/config/" + book.Name + "-" + book.Author + "/FrmReadConfig.json", json, Encoding.UTF8);
        }
    }
}
