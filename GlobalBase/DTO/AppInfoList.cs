using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBase.DTO
{
    public class AppInfoList
    {
        /// <summary>
        /// APPID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// APPName
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// APP 发布用户
        /// </summary>
        public string AppUser { get; set; }

        /// <summary>
        /// APP图标
        /// </summary>
        public string AppIcon { get; set; }

        /// <summary>
        /// APP启动图
        /// </summary>
        public string AppScreen { get; set; }

        /// <summary>
        /// APP类型 webapp,native,rn,flutter
        /// </summary>
        public string AppType { get; set; }

        /// <summary>
        /// APP 平台 IOS android
        /// </summary>
        public string AppPlatform { get; set; }

        /// <summary>
        /// APP 签名类型 IOS:webapp0, webappSSL,webappDev;android:apkv1,apkV2,apkv3
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
