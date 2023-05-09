using GlobalBase.DLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionTools.DTO
{
    public class PublicNoticeDTO
    {
        public string PublicNoticeID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        ///  有效期
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 状态: 1:上线,0:下线 -1:删除
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 公告查询
    /// </summary>
    public class PublicNoticeSelect
    {
        /// <summary>
        /// ID
        /// </summary>
        public string PublicNoticeID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 发布时间 开始
        /// </summary>
        [Operator(key: "Created", Operator: "GreaterEqual")]
        public DateTime? CreatedStart { get; set; }

        /// <summary>
        /// 发布时间 结束
        /// </summary>
        [Operator(key: "Created", Operator: "LessEqual")]
        public DateTime? CreatedEnd { get; set; }

        /// <summary>
        ///  有效期 开始
        /// </summary>
        [Operator(key: "Expiration", Operator: "GreaterEqual")]
        public DateTime? ExpirationStart { get; set; }

        /// <summary>
        /// 有效期 结束
        /// </summary>
        [Operator(key: "Expiration", Operator: "LessEqual")]
        public DateTime? ExpirationEnd { get; set; }

        /// <summary>
        /// 状态: 上线,下线
        /// </summary>
        [Operator(key: "Status", Operator: "GreaterEqual")]
        public int StatusStart { get; set; } = 0;

        /// <summary>
        /// 状态: 上线,下线
        /// </summary>
        [Operator(key: "Status", Operator: "LessEqual")]
        public int StatusEnd { get; set; } = 0;
    }


    /// <summary>
    /// 公告修改对象
    /// </summary>
    public class PublicNoticeUpdate
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public string PublicNoticeID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///  有效期
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 状态: 1:上线,0:下线 -1:删除
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 公告新增对象
    /// </summary>
    public class PublicNoticeAdd
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///  有效期
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 状态: 1上线,0下线
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}