using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Entities;

namespace GlobalBase.MongoModel
{
    /// <summary>
    /// 单位时间内统计
    /// </summary>
    public class WebConfigRecord : Entity, ICreatedOn
    {
        /// <summary>
        /// 自增数据 ID
        /// </summary>
        public string CustomID { get; set; }
        /// <summary>
        /// 配置 ID
        /// </summary>
        public string ConfigID { get; set; }

        /// <summary>
        /// 广告 ID
        /// </summary>
        public string AdID { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int Clicks { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }

    public class FloatConfigRecord : Entity, ICreatedOn
    {
        /// <summary>
        /// 自增数据 ID
        /// </summary>
        public string CustomID { get; set; }
        /// <summary>
        /// 配置 ID
        /// </summary>
        public string ConfigID { get; set; }

        // <summary>
        /// 广告 ID
        /// </summary>
        public string AdID { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int Clicks { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
