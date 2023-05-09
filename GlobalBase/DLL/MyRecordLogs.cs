using System;
using System.Linq;
using GlobalBase.DTO;
using LinqToDB.Common;
using MongoDB.Driver;
using MongoDB.Entities;
using PublicTools;

namespace GlobalBase.DLL
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public class MyRecordLogs
    {

        static string DataBaseName = GetConfig.GetConfigs("MongoDBConnection:DataBaseName");

        static MongoClientSettings settings = new MongoClientSettings()
        {
            Server = new MongoServerAddress(GetConfig.GetConfigs("MongoDBConnection:localhost"),
              Convert.ToInt32(GetConfig.GetConfigs("MongoDBConnection:port"))),
            Credential = MongoCredential.CreateCredential(DataBaseName,
              GetConfig.GetConfigs("MongoDBConnection:user"),
              GetConfig.GetConfigs("MongoDBConnection:pwd")),
            MaxConnectionPoolSize = int.Parse(GetConfig.GetConfigs("MongoDBConnection:MaxConnection"))
        };
        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="rId">操作记录ID</param>
        /// <param name="account">操作人账号</param>
        /// <param name="actionType">类型:新增/编辑</param>
        /// <param name="actionContent">操作内容:新增广告</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static async void WriteLogNew(string rId, string account, string actionType, string actionContent, string content)
        {

            await DB.InitAsync(DataBaseName, settings);
            try
            {
                var rlm = new RecordLog(); ;
                if (!string.IsNullOrEmpty(rId))
                {

                    rlm.RId = rId;
                }
                if (!string.IsNullOrEmpty(account))
                {

                    rlm.Account = account;
                }
                if (!string.IsNullOrEmpty(actionType))
                {

                    rlm.ActionType = actionType;
                }
                if (!string.IsNullOrEmpty(actionContent))
                {

                    rlm.ActionContent = actionContent;
                }
                if (!string.IsNullOrEmpty(content))
                {

                    rlm.Content = content;
                }
                await rlm.SaveAsync();
            }
            catch (Exception e)
            {
                var elError = new ErrorLog() { type = 9999, message = "日志记录操作：" + e.Message };
                await elError.SaveAsync();
            }

        }
    }

    /// <summary>
    /// 操作日志对象，存储到MongoDB
    /// </summary>
    public class RecordLog : Entity, ICreatedOn
    {
        // 操作人账号，类型，操作内容，内容，时间
        public string Account { get; set; }
        public string ActionType { get; set; }
        public string ActionContent { get; set; }
        public string RId { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}
