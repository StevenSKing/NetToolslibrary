using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using PublicTools;

namespace ExtensionTools.DLL
{
    /// <summary>
    /// DepperHelps
    /// </summary>
    public class DepperHelps
    {

        /// <summary>
        /// 异步执行SQL返回集合
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="param">参数model</param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> QueryAsync<T>(string strSql, object param)
        {
            using (IDbConnection conn = new SqlConnection(GetConfig.GetConfigs("MSSQLConnection:con0")))
            {
                try
                {
                    var dbset = await conn.QueryAsync<T>(strSql, param);
                    return dbset;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">查询的表名(多表查询 join lefe 连接)</param>
        /// <param name="files">查询的列</param>
        /// <param name="where">查询条件</param>
        /// <param name="param">条件关键字</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        public static IEnumerable<T> QueryMultiple<T>(string tablename, string files, string where, object param, string orderby, int pageIndex, int pageSize, out int total)
        {
            var skip = 0;
            if (pageIndex > 0)
            {
                skip = (pageIndex - 1) * pageSize;
            }
            var sb = new StringBuilder();
            sb.AppendFormat("select count(1) from {0} where {1};", tablename, where);
            sb.AppendFormat("select {0} from {1} where {2} order by {3} desc offset {4} rows fetch next {5} rows only", files, tablename, where, orderby, skip, pageSize);
            using (IDbConnection conn = new SqlConnection(GetConfig.GetConfigs("MSSQLConnection:con0")))
            {
                try
                {
                    var reader = conn.QueryMultiple(sb.ToString(), param);
                    total = reader.ReadFirst<int>();
                    return reader.Read<T>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strProcedure">过程名</param>
        /// <param name="param">参数</param>
        /// <returns>0成功，-1执行失败</returns>
        public static async Task<int> ExecuteStoredProcedure(string strProcedure, object param)
        {
            using (IDbConnection conn = new SqlConnection(GetConfig.GetConfigs("MSSQLConnection:con0")))
            {
                try
                {
                    return await conn.ExecuteAsync(strProcedure, param, null, null, CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

}
