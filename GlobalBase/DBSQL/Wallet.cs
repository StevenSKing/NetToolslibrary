using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace GlobalBase.DBSQL
{
    /// <summary>
    /// 钱包
    /// </summary>
    public class Wallet
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string Rid { get; set; }

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
        /// 用户钱包余额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 冻结金额
        /// </summary>
        public decimal LockMoney { get; set; }

        /// <summary>
        /// 用户支付密码
        /// </summary>
        [Required]
        public string GetMPwd { get; set; }
    }
}
