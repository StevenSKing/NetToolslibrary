
using GlobalBase.DBSQL;

using Microsoft.EntityFrameworkCore;

namespace GlobalBase.DLL
{

    /// <summary>
    /// 数据库原始操作
    /// </summary>
    public class InitiDataBase : DbContext
    {
        /// <summary>
        /// APP 下载记录表
        /// </summary>
        public DbSet<MobileRecord> MobileRecord { get; set; }

        /// <summary>
        /// APP 数据信息表
        /// </summary>
        public DbSet<AppData> Apps { get; set; }

        /// <summary>
        /// APP信息表
        /// </summary>
        public DbSet<AppInfo> AppInfo { get; set; }

        /// <summary>
        /// 用户信息表
        /// </summary>
        public DbSet<UserInfo> UserInfo { get; set; }

        /// <summary>
        /// 管理员信息表
        /// </summary>
        public DbSet<UserAdmin> UserAdmin { get; set; }

        /// <summary>
        /// 用户钱包表
        /// </summary>
        public DbSet<Wallet> Wallet { get; set; }




        private string DBconnection;

        public InitiDataBase(string conn)
        {
            DBconnection = conn;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(false)
                .UseSqlServer(DBconnection)
                //.EnableDetailedErrors(false)
                //.UseChangeTrackingProxies(false)
                ;

        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <returns></returns>
        public static bool InitDataBase(string conn)
        {
            // InitiDataBase dbContext = new InitiDataBase();
            using (var dbContext = new InitiDataBase(conn))
            {
                if (dbContext.Database.CanConnect())
                {
                    dbContext.Database.EnsureDeleted();
                }
                return dbContext.Database.EnsureCreated();
            }
        }
    }


}
