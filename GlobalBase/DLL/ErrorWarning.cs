using System;
using System.Linq;
using ExtensionTools.DTO;
using LinqToDB.Common;
using MongoDB.Driver;
using MongoDB.Entities;
using PublicTools;

namespace ExtensionTools.DLL
{
    /// <summary>
    /// 警报系统
    /// </summary>
    public class ErrorWarning
    {
        private static StatusMsg statusMsg = new StatusMsg();
        private static SendMail sm = new SendMail()
        {
            From = GetConfig.GetConfigs("EmailConfig:From"),
            Password = GetConfig.GetConfigs("EmailConfig:Password"),
            DisplayName = GetConfig.GetConfigs("EmailConfig:DisplayName"),
            Host = GetConfig.GetConfigs("EmailConfig:Host"),
            Port = Convert.ToInt32(GetConfig.GetConfigs("EmailConfig:Port"))
        };

        static int type = Convert.ToInt32(GetConfig.GetConfigs("ErrorWarning:Type"));
        static string emails = GetConfig.GetConfigs("ErrorWarning:Emails");
        static int interval = Convert.ToInt32(GetConfig.GetConfigs("ErrorWarning:ExIntervalpire"));
        static int maXCountHour = Convert.ToInt32(GetConfig.GetConfigs("ErrorWarning:MaXCountHour"));
        static int maXCountDay = Convert.ToInt32(GetConfig.GetConfigs("ErrorWarning:MaXCountDay"));

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
        /// 写入错误日志
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorType"></param>
        public static async void WriteError(string errorMessage, int errorType = 0)
        {

            await DB.InitAsync(DataBaseName, settings);
            if (errorType == 0) return;
            var now = DateTime.Now;
            try
            {
                //启用邮件警告
                if (type == 1)
                {
                    if (string.IsNullOrEmpty(emails)) return;
                    var emailList = emails.Split(',').ToList();
                    //收件人邮件地址错误，退出
                    if (emailList.IsNullOrEmpty()) return;
                    var CountHour = 0;
                    var CountDay = 0;

                    //启用时间限制,只限制同错误类型
                    if (interval > 0)
                    {
                        var time = now.AddMinutes(-interval);
                        var count = await DB.CountAsync<ErrorLog>(z => z.CreatedOn >= time && z.type == errorType);
                        //时间间隔内已发送过，退出
                        if (count > 0) return;
                    }
                    //启用小时最大次数限制,只限制同错误类型
                    if (maXCountHour > 0)
                    {
                        var time = now.AddHours(-1);
                        var count = await DB.CountAsync<ErrorLog>(z => z.CreatedOn >= time && z.type == errorType);
                        //1小时内达到最大发送次数，退出
                        if (count >= maXCountHour) return;
                    }
                    //启用小时最大次数限制,只限制同错误类型
                    if (maXCountDay > 0)
                    {
                        var time = now.AddDays(-1);
                        var count = await DB.CountAsync<ErrorLog>(z => z.CreatedOn >= time && z.type == errorType);
                        //1天内达到最大发送次数，退出
                        if (count >= maXCountDay) return;
                    }
                    //全部通过，本次可以发送邮件警告
                    foreach (var email in emailList)
                    {
                        if (VerifyCode.CheckEmail(email.Trim()))
                        {
                            sm.Addressee = email.Trim();
                            sm.Theme = "系统错误警告";
                            sm.Body = "错误类型：" + errorType + "<br/>" + errorMessage;
                            await PostEmail.LocalHostSend(sm);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var elError = new ErrorLog() { type = 999, message = "发送邮件警告出现错误：" + e.Message };
                elError.SaveAsync();
            }
            var el = new ErrorLog() { type = errorType, message = errorMessage };
            await el.SaveAsync();
        }
    }

    /// <summary>
    /// 错误日志对象，存储到MongoDB
    /// </summary>
    public class ErrorLog : Entity, ICreatedOn
    {
        /// <summary>
        /// 错误类型:……999=发送警告邮件出错
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 错误说明
        /// </summary>

        public string message { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}
