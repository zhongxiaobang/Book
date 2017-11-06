using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Model;
using System.IO;
using Book.Common;

namespace Book.Data
{
    /// <summary>
    /// 用户数据
    /// </summary>
    public class UserInfoData
    {
        public UserInfoData()
        {
                        
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUserInfos()
        {
            if (File.Exists("data/UserInfo.json"))
            {
                 return Kit.ToObject<List<UserInfo>>(FileUtils.ReadAllText("data/UserInfo.json", Encoding.UTF8));
            }
            return new List<UserInfo>();
        }

        /// <summary>
        /// 获得特定条件的用户信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<UserInfo> GetUserInfos(Func<UserInfo, bool> predicate)
        {
            return GetUserInfos().Where(predicate).ToList();
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userInfo"></param>
        public void Add(params UserInfo[] userInfo)
        {
            List<UserInfo> userInfos = GetUserInfos();
            userInfos.AddRange(userInfo);
            Save(userInfos);
        }

        /// <summary>
        /// 删除符合条件的用户
        /// </summary>
        /// <param name="match"></param>
        public void Delete(Func<UserInfo, bool> predicate)
        {
            List<UserInfo> userInfos = GetUserInfos();
            foreach (var item in userInfos.Where(predicate))
            {
                userInfos.Remove(item);
            }
            Save(userInfos);
        }

        /// <summary>
        /// 保存变化
        /// </summary>
        public void Save(List<UserInfo> userInfos)
        {
            string json = Kit.ToJson(userInfos, true);
            FileUtils.WriteAllText("data/UserInfo.json",json,Encoding.UTF8);
        }

    }
}
