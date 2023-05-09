using System;
using System.ComponentModel.DataAnnotations;

namespace GlobalBase.DBSQL
{
    public class AppData
    {

        /// <summary>
        /// 数据ID
        /// </summary>
        [Key]
        public string ADId { get; set; }
        /// <summary>
        /// APPID
        /// </summary>
        [Required]
        public string AppID { get; set; }

        /// <summary>
        /// APPName
        /// </summary>
        [Required]
        public string AppName { get; set; }


        /// <summary>
        /// APP 下载次数
        /// </summary>
        public int DownloadSum { get; set; } = 0;


        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime SettleTime { get; set; } = DateTime.Now;
    }
}
