using System;
using System.ComponentModel.DataAnnotations;

namespace GlobalBase.DTO
{
    public class UserList
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

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
        public string Msg { get; set; }

        /// <summary>
        /// 用户状态[2,3,4,5] 0封号、1正常
        /// </summary>
        [Required]
        public int State { get; set; } = 1;
    }
}
