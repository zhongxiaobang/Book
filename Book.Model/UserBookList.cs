using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Model
{
    /// <summary>
    /// 用户书籍列表
    /// </summary>
    public class UserBookList
    {
        public string ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 书籍ID
        /// </summary>
        public string BookID { get; set; }
    }
}
