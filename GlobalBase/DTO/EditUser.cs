using System.ComponentModel.DataAnnotations;

namespace ExtensionTools.DTO
{
    /// <summary>
    /// 用户修改自身信息
    /// </summary>
    public class EditUser
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
        /// 用户头像
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户状态[2,3,4,5] 0封号、1正常
        /// </summary>
        public int State { get; set; } = 1;
    }

    /// <summary>
    /// 用户列表
    /// </summary>
    public class SelectUser
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
        /// 用户状态[2,3,4,5] 0封号、1正常
        /// </summary>
        [Required]
        public int State { get; set; } = 1;
    }
}
