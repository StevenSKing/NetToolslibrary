using System.ComponentModel.DataAnnotations;

namespace ExtensionTools.DTO
{
    public class EditAdmin
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]//必填
        [StringLength(50)]
        public string Account { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Header { get; set; }

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
        /// 名称
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
        /// 用户状态：0=正常，-1=禁用
        /// </summary>
        public int Status { get; set; } = 0;
    }

    public class SelectAdmin
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
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
        /// 名称
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
        /// 用户状态：0=正常，-1=禁用
        /// </summary>
        public int Status { get; set; } = 0;
    }
}
