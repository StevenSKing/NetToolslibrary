using GlobalBase.DLL;

namespace GlobalBase.DTO
{
    /// <summary>
    /// 查询通用参数
    /// </summary>

    public class PaginationPrams
    {
        /// <summary>
        ///  0为当前页数，如果超过10个则可以通过页数进行分页
        /// </summary>       
        public int current { get; set; } = 1;
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pagesize { get; set; } = 10;

        /// <summary>
        /// 开始时间
        /// </summary>
        public string fist_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [PaginationPramsIsValid]
        public string last_time { get; set; }
    }

    /// <summary>
    /// 含页数的分页信息
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 每页的记录条数
        /// </summary>
        public int pagesize { get; set; } = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        public int current { get; set; } = 1;

        /// <summary>
        /// 开始时间
        /// </summary>
        public string fist_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string last_time { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int total { get; set; }
    }

    public class PaginationNoTimePrams
    {
        /// <summary>
        ///  0为当前页数，如果超过10个则可以通过页数进行分页
        /// </summary>       
        public int current { get; set; } = 1;
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pagesize { get; set; } = 10;
    }


    public class PaginationNoTime
    {
        /// <summary>
        /// 每页的记录条数
        /// </summary>
        public int pagesize { get; set; } = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        public int current { get; set; } = 1;

        /// <summary>
        /// 总数量
        /// </summary>
        public int total { get; set; }
    }

}


