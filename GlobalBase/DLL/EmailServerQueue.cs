// using System;
// using System.Collections.Concurrent;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net.Http;
// using System.Threading.Tasks;
// using GlobalBase.DTO;
// using MailKit.Net.Proxy;
// using MailKit.Net.Smtp;
// using MailKit.Security;
// using MimeKit;
// using MimeKit.Text;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;

// namespace GlobalBase.DLL
// {
//     public class EmailServerQueue
//     {
//         private readonly HttpClient Client;
//         private DateTime RTime;
//         public EmailServerQueue(IHttpClientFactory factory)
//         {
//             Client = factory.CreateClient();
//             RTime = DateTime.Now;
//         }
//         private static readonly ConcurrentQueue<DoveDTO> _doveQueue = new ConcurrentQueue<DoveDTO>();
//         public int doveCount => _doveQueue.Count;
//         public bool doveIsEmpty => _doveQueue.IsEmpty;
//         List<DoveDTO> doveList = new List<DoveDTO>();
//         public void DoveEnqueue(DoveDTO mailBox)
//         {
//             _doveQueue.Enqueue(mailBox);
//         }
//         public bool TryDoveDequeue(out DoveDTO? mailBox)
//         {
//             return _doveQueue.TryDequeue(out mailBox);
//         }

//         public bool SendEmail(string? to, string? subject, string? html, string? fromName, string? SmtpHost, int SmtpPort, string? SmtpUser, string? SmtpPass, string? from, out string eMsg)
//         {
//             try
//             {
//                 // if (doveIsEmpty || DateTime.Now >= RTime.AddMinutes(5))
//                 // {
//                 //     //美国=白名单
//                 //     // HttpResponseMessage doveReq = this.Client.GetAsync($"https://dvapi.doveip.com/cmapi.php?rq=distribute&user=toptop&token=eFJWaXROMlhReWVpTHErU1dmR0o3QT09&auth=0&geo=US&city=all&agreement=0&timeout=6&num=10&rtype=0").GetAwaiter().GetResult();
//                 //     // HttpResponseMessage doveReq = this.Client.GetAsync($"https://dvapi.doveip.com/cmapi.php?rq=distribute&user=toptop&token=eFJWaXROMlhReWVpTHErU1dmR0o3QT09&auth=0&geo=US&city=all&agreement=0&timeout=10&num=10&rtype=0").GetAwaiter().GetResult();
//                 //     HttpResponseMessage doveReq = this.Client.GetAsync($"https://dvapi.doveip.com/cmapi.php?rq=distribute&user=toptop&token=eFJWaXROMlhReWVpTHErU1dmR0o3QT09&auth=0&geo=GB&city=all&agreement=0&timeout=2&num=2&rtype=0").GetAwaiter().GetResult();


//                 //     //美国=密码
//                 //     // HttpResponseMessage doveReq = this.Client.GetAsync($"https://dvapi.doveip.com/cmapi.php?rq=distribute&user=toptop&token=eFJWaXROMlhReWVpTHErU1dmR0o3QT09&auth=1&geo=US&city=all&agreement=0&timeout=6&num=10&rtype=0").GetAwaiter().GetResult();
//                 //     if (doveReq.StatusCode == System.Net.HttpStatusCode.OK)
//                 //     {
//                 //         var doveRes = doveReq.Content.ReadAsStringAsync().GetAwaiter().GetResult();
//                 //         var doveData = "";
//                 //         var JsonObj = JsonConvert.DeserializeObject<JObject>(doveRes);

//                 //         var errno = JsonObj?["errno"]?.ToString();
//                 //         if (errno == "200")
//                 //         {
//                 //             try
//                 //             {
//                 //                 RTime = DateTime.Now;
//                 //                 doveData = JsonObj?["data"]?.ToString();
//                 //                 doveList = JsonConvert.DeserializeObject<List<DoveModel>>(doveData ?? "") ?? new List<DoveModel>();
//                 //                 foreach (var item in doveList)
//                 //                 {
//                 //                     DoveEnqueue(item);
//                 //                 }
//                 //             }
//                 //             catch (Exception ex)
//                 //             {
//                 //                 Logs.Error($"拿代理出现异常=>>{JsonObj?["msg"]?.ToString()} | {ex.Message}");
//                 //                 eMsg = $"拿代理出现异常";
//                 //                 return false;
//                 //             }
//                 //         }
//                 //         else
//                 //         {
//                 //             if (doveList.Count <= 0)
//                 //             {
//                 //                 Logs.Error($"拿代理出现异常2=>>{JsonObj?["msg"]?.ToString()}");
//                 //                 eMsg = $"拿代理出现异常2";
//                 //                 return false;
//                 //             }
//                 //         }
//                 //     }
//                 // }
//                 DoveDTO dove = new DoveDTO();
//                 //美国=白名单
//                 HttpResponseMessage doveReq = this.Client.GetAsync($"https://dvapi.doveip.com/cmapi.php?rq=distribute&user=toptop&token=eFJWaXROMlhReWVpTHErU1dmR0o3QT09&auth=0&geo=GB&city=all&agreement=0&timeout=2&num=1&rtype=0").GetAwaiter().GetResult();
//                 //美国=密码
//                 // HttpResponseMessage doveReq = this.Client.GetAsync($"https://dvapi.doveip.com/cmapi.php?rq=distribute&user=toptop&token=eFJWaXROMlhReWVpTHErU1dmR0o3QT09&auth=1&geo=US&city=all&agreement=0&timeout=6&num=10&rtype=0").GetAwaiter().GetResult();
//                 if (doveReq.StatusCode == System.Net.HttpStatusCode.OK)
//                 {
//                     var doveRes = doveReq.Content.ReadAsStringAsync().GetAwaiter().GetResult();
//                     var doveData = "";
//                     var JsonObj = JsonConvert.DeserializeObject<JObject>(doveRes);

//                     var errno = JsonObj?["errno"]?.ToString();
//                     if (errno == "200")
//                     {
//                         try
//                         {
//                             RTime = DateTime.Now;
//                             doveData = JsonObj?["data"]?.ToString();
//                             dove = JsonConvert.DeserializeObject<DoveDTO>(doveData ?? "") ?? new DoveDTO();
//                             // foreach (var item in doveList)
//                             // {
//                             //     DoveEnqueue(item);
//                             // }
//                         }
//                         catch (Exception ex)
//                         {
//                             eMsg = $"拿代理出现异常=>>{JsonObj?["msg"]?.ToString()} | {ex.Message}";
//                             throw new Exception(eMsg);
//                         }
//                     }
//                     else
//                     {
//                         eMsg = $"拿代理出现异常2=>>{JsonObj?["msg"]?.ToString()}";
//                         throw new Exception(eMsg);
//                     }
//                 }
//                 // create message
//                 var email = new MimeMessage();
//                 email.From.Add(new MailboxAddress($"{fromName}|{subject}@{Guid.NewGuid().ToString("N").Substring(5, 10)}.org", from));
//                 email.To.Add(MailboxAddress.Parse(to));
//                 email.MessageId = MimeKit.Utils.MimeUtils.GenerateMessageId();
//                 email.Subject = $"{fromName}|{subject}";

//                 var htmlTmp3 = $"<!doctype html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/><meta name=\"viewport\"  content=\"width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0\"><title>go java c# node ruby rust c++ c js</title><style type=\"text/css\">body{{background-color:yellow}}p{{color:blue}}img{{height:auto;width:auto\\9;width:100%}}</style></head><body>{html}</body></html>";
//                 email.Body = new TextPart(TextFormat.Html) { Text = htmlTmp3 };

//                 // send email
//                 using (var smtp = new SmtpClient())
//                 {
//                     smtp.CheckCertificateRevocation = false;
//                     smtp.ProxyClient = new Socks5Client(dove.IP, dove.Port);
//                     // TryDoveDequeue(out DoveModel? dove);
//                     // if (dove != null)
//                     // {
//                     //     smtp.ProxyClient = new Socks5Client(dove.IP, dove.Port);
//                     // }
//                     // else
//                     // {
//                     //     if (doveList.Count > 0)
//                     //     {
//                     //         var num = new Random().Next(doveList.Count);
//                     //         var _dove = doveList[num];
//                     //         smtp.ProxyClient = new Socks5Client(_dove.IP, _dove.Port);
//                     //         // smtp.ProxyClient = new Socks5Client(_dove.IP, _dove.Port, new NetworkCredential(_dove.UserName, _dove.Password));
//                     //     }
//                     // }
//                     // SystemLog.Error($"dove=>>{dove.GetJson()}");
//                     smtp.Connect(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
//                     smtp.Authenticate(SmtpUser, SmtpPass);
//                     smtp.Send(email);
//                     smtp.Disconnect(true);
//                     eMsg = "";
//                     return true;
//                 }
//             }
//             catch (SmtpCommandException ex)
//             {
//                 eMsg = $"{SmtpUser} | {SmtpPass} | 发送邮件小异常 | {ex.Source} | ErrorCode:{ex.ErrorCode}";
//                 throw new Exception(eMsg);
//             }
//             catch (Exception ex)
//             {
//                 eMsg = $"{SmtpUser} | 发送邮件大异常 | {ex.Source}";
//                 throw new Exception(eMsg);
//             }
//         }
//     }
// }