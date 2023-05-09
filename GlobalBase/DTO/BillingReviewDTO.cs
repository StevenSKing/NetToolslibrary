using System;
using System.ComponentModel.DataAnnotations;

using GlobalBase.DLL;

namespace GlobalBase.DTO;

/// <summary>
/// 新增结算单
/// </summary>
public class BillingReviewAdd
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required]
    public string Account { get; set; }

    /// <summary>
    /// 统计ID
    /// </summary>
    [Required]
    public string CountID { get; set; }


    /// <summary>
    /// 广告ID
    /// </summary>
    [Required]
    public string AdID { get; set; }

    /// <summary>
    /// 结算类型：Clicks(点击量), Views(访问量) Days(天数)
    /// </summary>
    [Required]
    public string BillingType { get; set; }

    /// <summary>
    /// 结算数量
    /// </summary>
    public string ADSum { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public string ADPrice { get; set; }

    /// <summary>
    /// 结算金额
    /// </summary>
    public int SettlementAmount { get; set; }

    /// <summary>
    /// 审核状态：1待处理，2通过，0拒绝 -删除
    /// </summary>
    [Required]
    public int Status { get; set; } = 1;
}

/// <summary>
/// 按countID批量结算
/// </summary>
public class BillingReviewAll
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 统计ID
    /// </summary>
    public string CountID { get; set; }


    /// <summary>
    /// 广告ID
    /// </summary>
    public string AdID { get; set; }

}

/// <summary>
/// 只可以删除状态为：[已提交] 的数据
/// </summary>
public class BillingReviewDelete
{
    /// <summary>
    /// 结算ID
    /// </summary>
    public string BillingReviewID { get; set; }
}

public class BillingReviewUpdate
{
    /// <summary>
    /// 结算ID
    /// </summary>
    [Required]
    public string BillingReviewID { get; set; }

    /// <summary>
    /// 广告ID
    /// </summary>
    [Required]
    public string AdID { get; set; }

    /// <summary>
    /// 审核状态：1待处理，2通过，0拒绝 -删除
    /// </summary>
    [Required]
    public int Status { get; set; } = 1;
}

/// <summary>
/// 查询
/// </summary>
public class BillingReviewSelect
{
    /// <summary>
    /// 结算ID
    /// </summary>
    public string BillingReviewID { get; set; }

    /// <summary>
    /// 配置ID
    /// </summary>
    public string ConfigID { get; set; }

    /// <summary>
    /// 统计ID
    /// </summary>
    public string CountID { get; set; }

    /// <summary>
    /// 广告ID
    /// </summary>
    public string AdID { get; set; }

    /// <summary>
    /// 结算类型：Clicks(点击量), Views(访问量) Days(天数)
    /// </summary>
    public string BillingType { get; set; }

    /// <summary>
    /// 状态: 上线,下线
    /// </summary>
    [Operator(key: "Status", Operator: "GreaterEqual")]
    public int StatusStart { get; set; } = 0;

    /// <summary>
    /// 状态: 上线,下线
    /// </summary>
    [Operator(key: "Status", Operator: "LessEqual")]
    public int StatusEnd { get; set; } = 0;


    /// <summary>
    /// 提交时间 开始
    /// </summary>
    [Operator(key: "Created", Operator: "GreaterEqual")]
    public DateTime? CreatedStart { get; set; }

    /// <summary>
    /// 提交时间 结束
    /// </summary>
    [Operator(key: "Created", Operator: "LessEqual")]
    public DateTime? CreatedEnd { get; set; }

}