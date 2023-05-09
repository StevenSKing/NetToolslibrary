using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace GlobalBase.DBSQL
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    /// 
    [Table("User")]
    public class UserInfo
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [Key]
        public string Rid { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? Header { get; set; }

        /// <summary>
        /// 用户的openid
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 上级分享码
        /// </summary>
        public string PCode { get; set; }

        /// <summary>
        /// 用户分享码
        /// </summary>
        [Required]
        public string ShareCode { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        [Required]
        public string Roles { get; set; }

        /// <summary>
        /// 用户状态[2,3,4,5] 0封号、1正常
        /// </summary>
        [Required]
        public int State { get; set; } = 1;

        /// <summary>
        /// 用户注册时间
        /// </summary>
        //[Required]
        public DateTime? RegDate { get; set; }

        /// <summary>
        /// 用户登陆时间
        /// </summary>
        //[Required]
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 用户备注
        /// </summary>
        public string? Msg { get; set; }
    }
}
