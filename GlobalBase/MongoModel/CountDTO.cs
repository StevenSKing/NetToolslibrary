using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBase.MongoModel
{
    public class CountDTO
    {
    }

    public class CountKey
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        public string ConfigID { get; set; }

        /// <summary>
        /// 统计ID
        /// </summary>
        public string CountID { get; set; }

        /// <summary>
        /// 选择的广告ID
        /// </summary>
        public string AdID { get; set; }
    }
}
