namespace GlobalBase.DLL
{
    /// <summary>
    /// 查询实体
    /// </summary>
    public class QueryEntity
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 操作方法，对应OperatorEnum枚举类
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 逻辑运算符，只支持AND、OR
        /// </summary>
        public string LogicalOperator { get; set; }
    }

    /// <summary>
    /// 操作方法枚举
    /// </summary>
    public enum OperatorEnum
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equals,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,

        /// <summary>
        /// 包含
        /// </summary>
        Contains,

        /// <summary>
        /// 由什么开始
        /// </summary>
        StartsWith,

        /// <summary>
        /// 由什么结束
        /// </summary>
        EndsWith,

        /// <summary>
        /// 大于
        /// </summary>
        Greater,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterEqual,

        /// <summary>
        /// 小于
        /// </summary>
        Less,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessEqual,
    }
}
