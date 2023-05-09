using System.Collections.Generic;

namespace GlobalBase.DTO
{
    /// <summary>
    /// 简易用户模型
    /// </summary>
    public class shortUserDTO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Header { get; set; } = "";
    }


    //token解析对象
    public class TokenDTO { 
    
      public string alg { get; set; }

        public string kid { get; set; }

        public string typ { get; set; }

        public string nbf { get; set; }

        public string exp { get; set; }

        public string iss { get; set; }

        public string aud { get; set; }

        public string client_id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string sub { get; set; }
        public string auth_time { get; set; }
       /// <summary>
       /// openID
       /// </summary>
        public string openID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string User { get; set; }
        public string UserType { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string role { get; set; }
        public string jti { get; set; }
        public string iat { get; set; }
        public List<string> scope { get; set; }
        public List<string> amr { get; set; }
       
    }









}
