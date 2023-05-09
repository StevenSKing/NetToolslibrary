using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalBase.DTO
{
    public class EmailRequestDTO
    {
        /// <summary>
        /// 接收邮箱
        /// </summary>
        /// <value></value>
        public string Email { get; set; }
        public int Type { get; set; }
        public string Sub { get; set; }
        public string FromName { get; set; }
        public string Content { get; set; }
    }
}