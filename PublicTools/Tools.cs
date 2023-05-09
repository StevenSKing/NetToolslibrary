using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using NETCore.Encrypt;

namespace PublicTools
{
    public static class Tools
    {

        /// <summary>
        /// 正则判断是否包含特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSpecialChar(string str)
        {
            var regExp = new Regex("[^0-9a-zA-Z\u4e00-\u9fa5]");
            if (regExp.IsMatch(str))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// HMACMD5加密
        /// </summary>
        /// <param name="source">加密消息体</param>
        /// <param name="key">加密秘钥</param>
        /// <returns></returns>
        public static string HmacMD5(string source, string key)
        {
            var utf8 = new UTF8Encoding();
            var hmacmd = new HMACMD5(utf8.GetBytes(key));
            var inArray = hmacmd.ComputeHash(utf8.GetBytes(source));
            var sb = new StringBuilder();

            for (var i = 0; i < inArray.Length; i++)
            {
                sb.Append(inArray[i].ToString("X2"));
            }

            hmacmd.Clear();

            return sb.ToString();
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            var response = (HttpWebResponse)request.GetResponse();

            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            var retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// 发送请求GET
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetRequest(string url, string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json; charset=utf-8 ";


            var response = (HttpWebResponse)request.GetResponse();

            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            var retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }


        /// <summary>
        /// 发送请求POST
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostRequest(string url, string url_, string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json; charset=utf-8 ";
            var bs = Encoding.UTF8.GetBytes(url_.ToString());//UTF-8          
            request.ContentLength = bs.Length;
            using (var reqStream = request.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();

            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            var retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }



        /// <summary>
        /// 签名HMacMD5验证对比
        /// </summary>
        /// <param name="info">订单信息</param>
        /// <param name="MD5">签名</param>
        /// <param name="secret">秘钥</param>
        /// <returns></returns>
        public static bool VerifyInfo(string info, string MD5, string secret)
        {
            var TrueMD5 = HmacMD5(info, secret);

            if (TrueMD5 == MD5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 校验token
        /// </summary>
        /// <returns></returns>
        public static bool CheckToken(string e, string n, string token)
        {
            var param = new RSAParameters()
            {
                // 参数为获取公钥返回结果的 e 自动
                Exponent = FromBase64Url(e),
                // 参数为获取公钥返回结果的 n 自动
                Modulus = FromBase64Url(n),
            };

            var rSA = RSA.Create(param);
            var tokens = token.Split(".");
            // 第一个参数为返回token用 '.' 截取的第一段和第二段，即jwt token 头和数据部分
            var token1 = tokens[0] + tokens[1];
            var token2 = tokens[2];
            // 第二个参数为返回token用 '.' 截取的第三段，即jwt token签名
            var ispass = rSA.VerifyData(UTF8Encoding.UTF8.GetBytes(token1),
                FromBase64Url(token2),
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);
            return ispass;
        }

        public static byte[] FromBase64Url(string base64Url)
        {
            var padded = base64Url.Length % 4 == 0
              ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            var base64 = padded.Replace("_", "/").Replace("-", "+");
            return Convert.FromBase64String(base64);
        }

        public static string getMd5(string plainTest)
        {
            var md5 = MD5.Create();
            var bytStr = md5.ComputeHash(Encoding.UTF8.GetBytes(plainTest));
            var encryptStr = "";
            for (var i = 0; i < bytStr.Length; i++)
            {
                encryptStr = encryptStr + bytStr[i].ToString("x").PadLeft(2, '0');
            }
            return encryptStr;
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecrypt(string data, string key)
        {
            return EncryptProvider.AESDecrypt(data, key);
        }

        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEncrypt(string data, string key)
        {
            return EncryptProvider.AESEncrypt(data, key);
        }

    }
}
