using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PublicTools
{
    public class VerifyCode
    {
        /// <summary>
        /// 生成随机验证码 大小写字母+数字 （不会出现相邻字符相同的情况）
        /// </summary>
        /// <param name="VcodeNum">位数</param>
        /// <returns></returns>
        public static string RndNum(int VcodeNum)
        {
            //验证码可以显示的字符集合  
            var Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p" +
                        ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q" +
                        ",R,S,T,U,V,W,X,Y,Z";
            var VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组   
            var code = "";//产生的随机数  
            var temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数  

            var rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同  
            for (var i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类  
                }
                var t = rand.Next(VcArray.Length);//获取随机数  
                if (temp != -1 && temp == t)
                {
                    return RndNum(VcodeNum);//如果获取的随机数重复，则递归调用  
                }
                temp = t;//把本次产生的随机数记录起来  
                code += VcArray[t];//随机数的位数加一  
            }
            return code;
        }

        /// <summary>
        /// 生成随机数字  示例
        /// </summary>
        /// <param name="VcodeNum">位数</param>
        /// <returns></returns>
        public static string RndOnlyNum(int VcodeNum)
        {
            //验证码可以显示的字符集合  
            var Vchar = "0,1,2,3,4,5,6,7,8,9";
            var VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组   
            var code = "";//产生的随机数  
            var temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数  

            var rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同  
            for (var i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类  
                }
                var t = rand.Next(VcArray.Length);//获取随机数  
                if (temp != -1 && temp == t)
                {
                    return RndOnlyNum(VcodeNum);//如果获取的随机数重复，则递归调用  
                }
                temp = t;//把本次产生的随机数记录起来  
                code += VcArray[t];//随机数的位数加一  
            }
            return code;
        }

        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen"></param>
        /// <returns></returns>
        #region 生成随机字符码
        public string CreateVerifyCode(int codeLen)
        {
            if (codeLen == 0)
            {
                codeLen = Length;
            }

            var arr = CodeSerial.Split(',');

            var code = "";


            var rand = new Random(unchecked((int)DateTime.Now.Ticks));

            for (var i = 0; i < codeLen; i++)
            {
                var randValue = rand.Next(0, arr.Length - 1);

                code += arr[randValue];
            }

            return code;
        }

        public string CreateVerifyCode()
        {
            return CreateVerifyCode(0);
        }
        #endregion

        #region 验证码长度(默认4个验证码的长度)

        public int Length { get; set; } = 4;

        #endregion

        #region 验证码字体大小(为了显示扭曲效果，默认40像素，可以自行修改)

        public int FontSize { get; set; } = 40;

        #endregion

        #region 边框补(默认1像素)

        public int Padding { get; set; } = 2;

        #endregion

        #region 是否输出燥点(默认不输出)

        public bool Chaos { get; set; } = true;

        #endregion

        #region 输出燥点的颜色(默认灰色)

        public Color ChaosColor { get; set; } = Color.LightGray;

        #endregion

        #region 自定义背景色(默认白色)

        public Color BackgroundColor { get; set; } = Color.White;

        #endregion

        #region 自定义随机颜色数组

        public Color[] Colors { get; set; } = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

        #endregion

        #region 自定义字体数组

        public string[] Fonts { get; set; } = { "Georgia", "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

        #endregion

        #region 自定义随机码字符串序列(使用逗号分隔)

        public string CodeSerial { get; set; } = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        #endregion

        #region 产生波形滤镜效果
        private const double Pi = 3.1415926535897932384626433832795;
        private const double Pi2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        /// </summary>
        /// <param name="sourceBitmap">图片路径</param>
        /// <param name="xDir">如果扭曲则选择为True</param>
        /// <param name="multValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="phase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public Bitmap TwistImage(Bitmap sourceBitmap, bool xDir, double multValue, double phase)
        {
            var destBmp = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            // 将位图背景填充为白色
            var graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            var dBaseAxisLen = xDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (var i = 0; i < destBmp.Width; i++)
            {
                for (var j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = xDir ? (Pi2 * (double)j) / dBaseAxisLen : (Pi2 * (double)i) / dBaseAxisLen;
                    dx += phase;
                    var dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = xDir ? i + (int)(dy * multValue) : i;
                    nOldY = xDir ? j : j + (int)(dy * multValue);

                    var color = sourceBitmap.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
        #endregion

        #region 生成校验码图片
        public Bitmap CreateImageCode(string code)
        {
            var fSize = FontSize;
            var fWidth = fSize + Padding;

            var imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
            var imageHeight = fSize * 2 + Padding;

            var image = new Bitmap(imageWidth, imageHeight);

            var g = Graphics.FromImage(image);

            g.Clear(BackgroundColor);

            var rand = new Random();

            //给背景添加随机生成的燥点
            if (this.Chaos)
            {

                var pen = new Pen(ChaosColor, 0);
                var c = Length * 10;

                for (var i = 0; i < c; i++)
                {
                    var x = rand.Next(image.Width);
                    var y = rand.Next(image.Height);

                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }


            var n1 = (imageHeight - FontSize - Padding * 2);
            var n2 = n1 / 4;
            var top1 = n2;
            var top2 = n2 * 2;


            //随机字体和颜色的验证码字符
            for (var i = 0; i < code.Length; i++)
            {
                var cindex = rand.Next(Colors.Length - 1);
                var findex = rand.Next(Fonts.Length - 1);

                var font = new Font(Fonts[findex], fSize, FontStyle.Bold);
                var brush = new SolidBrush(Colors[cindex]);

                var top = i % 2 == 1 ? top2 : top1;

                var left = i * fWidth;

                g.DrawString(code.Substring(i, 1), font, brush, left, top);
            }

            //画一个边框 边框颜色为Color.Gainsboro
            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();

            //产生波形（Add By 51aspx.com）
            image = TwistImage(image, true, 8, 4);

            return image;
        }
        #endregion

        #region 将创建好的图片输出到页面
        public byte[] CreateImage(string code)
        {

            var ms = new MemoryStream();
            var image = this.CreateImageCode(code);
            try
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
            finally
            {
                ms.Close();
                ms = null;
                image.Dispose();
                image = null;
            }

        }
        #endregion

        /// <summary>
        /// 根据随机数生成图片，写入数据流   示例
        /// </summary>
        /// <param name="code"></param>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static MemoryStream Create(out string code, int numbers = 4)
        {
            code = RndNum(numbers);
            //Bitmap img = null;
            //Graphics g = null;
            MemoryStream ms = null;
            var random = new Random();
            //验证码颜色集合  
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };


            using (var img = new Bitmap((int)code.Length * 18, 32))
            {
                using (var g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);//背景设为白色

                    //在随机位置画背景点  
                    for (var i = 0; i < 100; i++)
                    {
                        var x = random.Next(img.Width);
                        var y = random.Next(img.Height);
                        g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
                    }
                    //验证码绘制在g中  
                    for (var i = 0; i < code.Length; i++)
                    {
                        var cindex = random.Next(7);//随机颜色索引值  
                        var findex = random.Next(5);//随机字体索引值  
                        var f = new Font(fonts[findex], 15, FontStyle.Bold);//字体  
                        Brush b = new SolidBrush(c[cindex]);//颜色  
                        var ii = 4;
                        if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                        {
                            ii = 2;
                        }
                        g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii);//绘制一个验证字符  
                    }
                    ms = new MemoryStream();//生成内存流对象  
                    img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中  
                }
            }

            return ms;
        }

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

        public string MD516(string strPwd)
        {

            var md5 = new MD5CryptoServiceProvider();
            var t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(strPwd)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2.ToLower();
        }

        /* 通过正则表达式在对邮箱进行认证时候,一些错误的邮箱格式仍能通过.

 原因是在使用System.Text.RegularExpressions.Regex.IsMatch(string s) 方法返回的是, 匹配项

 这里的匹配项是指在输入的字符串s中存在符合条件的值(这个值可以为s中的部分匹配项, 而我们需要的是这个S字符串是否满足条件所以不能通过这个方式判断)

 这里使用的方法是通过正则表达式返回匹配项,然后判断输入的字符串s是否在匹配项中,不存在则不满足条件,反之满足*/

        /// <summary>
        /// 正则表达式检测Email格式
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static bool CheckEmail(string Email)
        {
            var Flag = false;
            var str = "([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,5})+";
            var result = GetPathPoint(Email, str);
            if (result != null)
            {
                Flag = result.Contains(Email) ? true : Flag;
            }
            return Flag;
        }

        /// <summary>
        /// 获取正则表达式匹配结果集
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regx">正则表达式</param>
        /// <returns></returns>
        public static string[] GetPathPoint(string value, string regx)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            var isMatch = Regex.IsMatch(value, regx);
            if (!isMatch)
                return null;
            var matchCol = Regex.Matches(value, regx);
            var result = new string[matchCol.Count];
            if (matchCol.Count > 0)
            {
                for (var i = 0; i < matchCol.Count; i++)
                {
                    result[i] = matchCol[i].Value;
                }
            }
            return result;
        }
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="phone"></param>       
        public static bool CheckPhone(string phone)
        {
            return Regex.IsMatch(phone, "^(0\\d{2,3}-?\\d{7,8}(-\\d{3,5}){0,1})|(((13[0-9])|(15([0-3]|[5-9]))|(18[0-9])|(17[0-9])|(14[0-9]))\\d{8})$");
        }

    }
}