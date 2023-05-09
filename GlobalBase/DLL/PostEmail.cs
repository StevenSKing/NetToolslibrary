using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ExtensionTools.DTO;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ExtensionTools.DLL
{
    public class SendMail
    {
        public string From { get; set; }//发件人地址      
        public string Password { get; set; }//密码
        public string Addressee { get; set; }//收件人地址集合
        public string[] CC { get; set; }//抄送
        public string Theme { get; set; }//主题
        public string DisplayName { get; set; }//发件人名称
        public Encoding SubjectEncoding { get; set; } = Encoding.UTF8;//编码
        public string Body { get; set; }//邮件内容
        public Encoding BodyEncoding { get; set; } = Encoding.UTF8;//邮件内容编码
        public bool IsBodyHtml { get; set; } = true;//是否HTML邮件
        public MailPriority Priority { get; set; } = MailPriority.Normal;//邮件优先级
        public bool EnableSsl { get; set; } = true;//是否ssl
        public bool UseDefaultCredentials { get; set; }
        public string Host { get; set; }//域名
        public int Port { get; set; }//端口       
    }

    public class PostEmail
    {
        //    public static async Task<StatusMsg> LocalHostSend(SendMail M)
        //    {
        //        StatusMsg msg = new StatusMsg();
        //        try
        //        {
        //            System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage();//发送电子邮件类
        //            if (M.Addressee != null)
        //            {
        //                foreach (string item in M.Addressee)//添加收件人
        //                {
        //                    myMail.To.Add(item);
        //                }
        //            }
        //            if (M.CC != null)
        //            {
        //                foreach (string item in M.CC)//添加抄送
        //                {
        //                    myMail.CC.Add(item);
        //                }
        //            }

        //            myMail.Subject = M.Theme;//邮件主题
        //            myMail.SubjectEncoding = M.SubjectEncoding;//邮件标题编码

        //            myMail.From = new MailAddress(M.From, M.DisplayName, M.SubjectEncoding);//发件信息
        //            myMail.Body = M.Body;//邮件内容
        //            myMail.BodyEncoding = M.BodyEncoding;//邮件内容编码
        //            myMail.IsBodyHtml = M.IsBodyHtml;//是否是HTML邮件
        //            myMail.Priority = M.Priority;//邮件优先级

        //            SmtpClient smtp = new SmtpClient();//SMTP协议

        //            smtp.EnableSsl = M.EnableSsl;//是否使用SSL安全加密 使用QQ邮箱必选
        //            smtp.UseDefaultCredentials = M.UseDefaultCredentials;
        //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //指定电子邮件发送方式
        //            smtp.Host = M.Host;//主机
        //            smtp.Port = M.Port;
        //            smtp.Credentials = new NetworkCredential(M.From, M.Password);//验证发件人信息

        //            smtp.Send(myMail);//发送
        //            msg.Status = true;
        //            return msg;
        //        }
        //        catch (Exception e)
        //        {
        //            msg.Msg = "发送失败： " + (e.Message);
        //            msg.Status = false;
        //            return msg;
        //        }
        //    }

        public static async Task<StatusMsg> LocalHostSend(SendMail M)
        {
            var msg = new StatusMsg();
            try
            {
                var name = Encoding.Default.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Default, Encoding.Default.GetBytes(M.DisplayName), 0, Encoding.Default.GetBytes(M.DisplayName).Length));
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(name, M.From));
                message.To.Add(new MailboxAddress("尊敬的用户", M.Addressee));
                message.Subject = M.Theme;
                //html or plain
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = M.Body;
                //bodyBuilder.TextBody = M.Body;

                var body = bodyBuilder.ToMessageBody();

                message.Body = body;

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    //smtp服务器，端口，是否开启ssl
                    client.Connect(M.Host, M.Port, M.EnableSsl);
                    client.Authenticate(M.From, M.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
                msg.Status = true;
                return msg;
            }
            catch (Exception e)
            {
                msg.Msg = "发送失败： " + e.Message;
                msg.Status = false;
                return msg;
            }
        }
    }
}
