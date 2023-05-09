using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtensionTools.DBSQL
{
    /// <summary>
    /// 管理员账号
    /// </summary>
    public class UserAdmin
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        public string TenantID { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 账号
        /// </summary>
        [Required]//必填
        [StringLength(50)]
        public string Account { get; set; } = "";

        /// <summary>
        /// 工号
        /// </summary>
        public string NUmber { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>        
        [StringLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Header { get; set; } = "";

        /// <summary>
        /// 权限
        /// </summary>
        [Required]
        public string Role { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>        
        public string WeChat { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>        
        public string QQ { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>        
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 标记
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? RegTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime? LoginTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 用户状态：0=正常，-1=禁用
        /// </summary>
        [Required]
        public int Status { get; set; } = 0;

    }





}
