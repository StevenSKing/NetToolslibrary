using GlobalBase.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionTools.DTO
{
    public class RecordLogDTO
    {
    }

    /// <summary>
    /// 日志查询参数
    /// </summary>
    public class RecordLogQureDTO
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 操作类型:编辑/新增
        /// </summary>
        public string actionType { get; set; }

        /// <summary>
        /// 操作时间 开始
        /// </summary>
        [Operator(key: "Created", Operator: "GreaterEqual")]
        public DateTime? CreatedStart { get; set; }

        /// <summary>
        /// 操作时间 结束
        /// </summary>
        [Operator(key: "Created", Operator: "LessEqual")]
        public DateTime? CreatedEnd { get; set; }
    }
}
