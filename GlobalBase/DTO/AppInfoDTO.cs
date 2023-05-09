using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBase.DTO
{
    /// <summary>
    /// 新增APP信息
    /// </summary>
    public class NewAppInfoDTO
    {

        /// <summary>
        /// APPName
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// H5 APP的地址
        /// </summary>
        public string AppURL { get; set; }

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

    }

    /// <summary>
    /// APP信息更新
    /// </summary>
    public class SetAppInfoDTO
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
        /// H5 APP的地址
        /// </summary>
        public string AppURL { get; set; }

        /// <summary>
        /// APP图标
        /// </summary>
        public string AppIcon { get; set; }

        /// <summary>
        /// APP启动图
        /// </summary>
        public string AppScreen { get; set; }

        /// <summary>
        /// APP 下载地址
        /// </summary>
        public string DownloadURL { get; set; }

    }
}
