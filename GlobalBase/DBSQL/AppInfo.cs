

using System;
using System.ComponentModel.DataAnnotations;

namespace GlobalBase.DBSQL
{
    /// <summary>
    /// 用户应用
    /// </summary>
    public class AppInfo
    {

        /// <summary>
        /// APPID
        /// </summary>
        [Key]
        public string AppID { get; set; }

        /// <summary>
        /// APPName
        /// </summary>
        [Required]
        public string AppName { get; set; }

        /// <summary>
        /// APP 发布用户
        /// </summary>
        [Required]
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
        /// H5 APP的地址
        /// </summary>
        public string AppURL { get; set; }

        /// <summary>
        /// APP类型 webapp,native,rn,flutter
        /// </summary>
        public string AppType { get; set; }

        /// <summary>
        /// APP 平台 IOS android
        /// </summary>
        public string AppPlatform { get; set; }

        /// <summary>
        /// APP 下载地址
        /// </summary>
        public string DownloadURL { get; set; }

        /// <summary>
        /// APP 签名类型 IOS:webapp0, webappSSL,webappDev;android:apkv1,apkV2,apkv3
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// APP 下载次数
        /// </summary>
        public int DownloadSum { get; set; }

        /// <summary>
        /// APP 状态 0:等待处理 1:正在封装 2：已封装 3：已上架 4：已下架
        /// </summary>
        public int AppState { get; set; }

        /// <summary>
        /// 上次结算时间
        /// </summary>
        public DateTime CutTime { get; set; } 

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


    }
}
