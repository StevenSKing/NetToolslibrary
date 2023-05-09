using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBase.EnumMode
{
    public class Platfrom
    {
        public List<string> PlatfromList { get; set; } = new List<string>()
        {
        "ALL",
        "Mobile",
        "PC",
        "Android",
        "IOS",
        "Windows",
        "OSX"
         };


        /// <summary>
        /// 全平台
        /// </summary>
        public string ALL { get; set; } = "ALL";

        /// <summary>
        /// 手机端
        /// </summary>
        public string Mobile { get; set; } = "Mobile";

        /// <summary>
        /// 电脑端
        /// </summary>
        public string PC { get; set; } = "PC";

        /// <summary>
        /// 安卓平台
        /// </summary>
        public string Android { get; set; } = "Android";

        /// <summary>
        /// IOS平台
        /// </summary>
        public string IOS { get; set; } = "IOS";

        /// <summary>
        /// windows平台
        /// </summary>
        public string Windows { get; set; } = "Windows";

        /// <summary>
        /// OSX平台
        /// </summary>
        public string OSX { get; set; } = "OSX";
    }











}
