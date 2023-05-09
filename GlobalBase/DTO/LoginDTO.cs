using System.Collections.Generic;
using NETCore.Encrypt.Internal;

namespace ExtensionTools.DTO
{/// <summary>
 /// 登录返回报文
 /// </summary>

    public class LoginDTO
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public string TenantID { get; set; }

        /// <summary>
        /// 用户账号（手机号，邮箱，自定义）
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public List<string> Role { get; set; }

        /// <summary>
        /// 用户状态 0封号。1正常
        /// </summary>
        public int state { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Msg { get; set; }


    }

    /// <summary>
    /// 账号密码正常登陆模型）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LoginNDTO
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

    }


    public class TokenSign
    {
        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public AESKey Sign { get; set; }

    }

}
