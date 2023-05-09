using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace ExtensionTools.DTO
{
    class GlobalDTO
    {
    }

    /// <summary>
    /// 返回结果结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class R<T>
    {

        public R()
        {
        }

        public R(T v)
        {
            data = v;
            success = true;
        }

        /// <summary>
        /// 返回提示信息
        /// </summary>
        public string msg { get; set; } = "";
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
        /// <summary>
        /// 返回状态
        /// 正确为true 不正确为false
        /// </summary>
        public bool success { get; set; } = false;

    }


    /// <summary>
    /// 状态消息模型
    /// </summary>
    public class StatusMsg
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; } = false;
        /// <summary>
        /// 状态附带消息
        /// </summary>
        public string Msg { get; set; }

    }

    /// <summary>
    /// 数据消息模型
    /// </summary>
    public class DataMsg<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 状态附带消息
        /// </summary>
        public T Data { get; set; }

    }
    /// <summary>
    /// 图形验证码模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class VerifyImgDTO
    {
        /// <summary>
        ///验证码图片字节流
        /// </summary>
        public FileContentResult Byteimage { get; set; }

        /// <summary>
        /// 验证码ID
        /// </summary>
        public string CID { get; set; }
    }

    /// <summary>
    /// 用户手动注册模型
    /// </summary>
    public class RegUserDto
    {

        /// <summary>
        /// 上级分享码
        /// </summary>
        [Required]//必填
        public string ParentCode { get; set; } = "0";

        /// <summary>
        /// 用户账号
        /// </summary>
        /// 
        [Required]//必填
        public string Account { get; set; }

        /// <summary>
        ///名称
        /// </summary>
        /// 
        public string Nick { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 验证码（图形验证码，短信验证码，邮箱验证码）
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 验证码ID（图形验证码，短信验证码，邮箱验证码）
        /// </summary>
        public string VerifyID { get; set; }
        /// <summary>
        /// 外部平台注册需带上clientId，验证中心注册字段留空
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public List<string> Roles { get; set; }

    }

    /// <summary>
    /// 用户简易注册模型
    /// </summary>
    public class RegUserEasy
    {

        /// <summary>
        /// 上级分享码
        /// </summary>
        [Required]//必填
        public string ParentCode { get; set; } = "0";

        /// <summary>
        /// 用户账号
        /// </summary>
        /// 
        [Required]//必填
        public string Account { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        [Required]//必填
        public string Pwd { get; set; }

        /// <summary>
        /// 验证码（图形验证码，短信验证码，邮箱验证码）
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 验证码ID（图形验证码，短信验证码，邮箱验证码）
        /// </summary>
        public string VerifyID { get; set; }

    }

    /// <summary>
    /// 找回密码模型
    /// </summary>
    public class FindPwdDto
    {
        /// <summary>
        /// 账号,邮箱,手机号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; } = "";
    }
    public class BindingEmailDto
    {
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
    }

    public class BindingPhoneDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
    }

    public class GetTokenDTO
    {
        public string Id { get; set; }

        public string Secret { get; set; }
    }

    public class WSResultDTO
    {
        /// <summary>
        /// 接口名
        /// </summary>
        public string service { get; set; }

        /// <summary>
        /// 返回代码 0成功,其他失败
        /// </summary>
        public int result_code { get; set; }

        /// <summary>
        /// 返回描述
        /// </summary>
        public string message { get; set; }

    }

    /// <summary>
    /// 分页模型
    /// </summary>
    public class PaginationDto<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> data { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 返回描述
        /// </summary>
        public string msg { get; set; }

    }
    public class FuncForID
    {
        public string ID { get; set; }
    }

    public class SwichState
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
    }
}
