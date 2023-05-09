using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBase.DTO
{
    public class AppDataList
    {
        /// <summary>
        /// APPID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// APP 发布用户
        /// </summary>
        public string AppUser { get; set; }

        /// <summary>
        /// APPName
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// APP 平台 IOS android
        /// </summary>
        public string AppPlatform { get; set; }

        /// <summary>
        /// 状态：0不可用，1正常、-1删除
        /// </summary>
        public int Status { get; set; } = 1;
    }
}
