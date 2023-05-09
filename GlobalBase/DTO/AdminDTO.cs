using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlobalBase.DTO
{
    public class AdminDTO
    {
    }
    /// <summary>
    /// 管理员注册模型
    /// </summary>
    public class AdminRegDto
    {

        /// <summary>
        /// 账号
        /// </summary>
        [Required]//必填
        [StringLength(50)]
        public string Account { get; set; } = "";
        /// <summary>
        /// 手机号
        /// </summary>        
        [StringLength(20)]
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
       
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 所属项目ClienID
        /// </summary>        
        public string ProRole { get; set; } = "";

        /// <summary>
        /// 权限
        /// </summary>
        public string Role { get; set; } = "Admin";

        /// <summary>
        /// 备注
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 标记
        /// </summary>
        public string Tag { get; set; }
    }

    /// <summary>
    /// 管理员登录返回模型
    /// </summary>
    public class AdminLoginDTO
    {

        public string TenantID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Header { get; set; } = "";

        /// <summary>
        /// 工号
        /// </summary>
        public string NUmber { get; set; } = "";

        /// <summary>
        /// 权限
        /// </summary>
        public List<string> Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; } = "";

        /// <summary>
        /// 名称
        /// </summary>
        public string RealName { get; set; } = "";

        /// <summary>
        /// 手机号
        /// </summary>        
        public string Phone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Msg { get; set; } = "";
        /// <summary>
        /// 标记
        /// </summary>
        public string Tag { get; set; } = "";

        /// <summary>
        /// 用户状态：0=正常，-1=禁用
        /// </summary>
        public int Status { get; set; } = 0;
    }
}
