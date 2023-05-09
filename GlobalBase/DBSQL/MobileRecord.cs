using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBase.DBSQL
{
    public class MobileRecord
    {
        /// <summary>
        ///下载记录ID
        /// </summary>
        [Key]
        public string RID { get; set; }
        /// <summary>
        /// APPID
        /// </summary>
        [Required]
        public string AppID { get; set; }

        /// <summary>
        /// 手机名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作系统版本 
        /// </summary>
        public string OSVersion { get; set; }

        /// <summary>
        /// 手机标识
        /// </summary>
        public string IEMI { get; set; }


        /// <summary>
        /// 手机浏览器指纹
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// 手机屏幕高度
        /// </summary>
        public string screenHeight { get; set; }

        /// <summary>
        /// 手机屏幕宽度
        /// </summary>
        public string screenWidth { get; set; }

        /// <summary>
        /// 手机通用唯一标识
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        /// 手机语言版本
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
