using System.ComponentModel.DataAnnotations;

namespace ExtensionTools.DTO
{
    public class WalletDTO
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]//必填
        [StringLength(50)]
        public string Account { get; set; } = "";

        /// <summary>
        /// 用户钱包地址
        /// </summary>
        public string RAdrUsdt { get; set; }


        /// <summary>
        /// 用户支付密码
        /// </summary>
        public string GetMPwd { get; set; }
    }

    public class WalletInfoDTO
    {
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(50)]
        public string Account { get; set; } = "";

        /// <summary>
        /// 用户钱包地址
        /// </summary>
        public string RAdrUsdt { get; set; }


        /// <summary>
        /// 用户余额
        /// </summary>
        public decimal Money { get; set; }
    }
}
