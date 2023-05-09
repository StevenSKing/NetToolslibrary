using System;

namespace PublicTools
{
    /// <summary>
    /// 将时间戳转为时间
    /// </summary>
    public class TimeTools
    {
        /// <summary>
        /// string的时间戳(只支持秒和毫秒级别的时间戳)
        /// </summary>
        /// <param name="strtime"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string strtime)
        {

            var startTime = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
            var digit = strtime.Length == 10 ? "0000000" : strtime.Length == 13 ? "0000" : "";
            var lotime = long.Parse(strtime + digit);
            var timeSpan = new TimeSpan(lotime);
            return startTime.Add(timeSpan);
        }
        /// <summary>
        /// 传的long时间戳(只支持秒和毫秒级别的时间戳)
        /// </summary>
        /// <param name="loTime"></param>
        /// <returns></returns>

        public static DateTime GetDateTime(long loTime)
        {
            var dtStart = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
            long digit = loTime.ToString().Length == 10 ? 10000000 : loTime.ToString().Length == 13 ? 10000 : 1;
            var lTime = (loTime * digit);
            var toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 传的int时间戳(只支持秒和毫秒级别的时间戳)
        /// </summary>
        /// <param name="intTime"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(int intTime)
        {
            var dtStart = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
            long digit = intTime.ToString().Length == 10 ? 10000000 : intTime.ToString().Length == 13 ? 10000 : 1;
            var lTime = ((long)intTime * digit);
            var toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 获取时间戳（毫秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStapmMilliseconds(string dateTime)
        {
            var time = DateTime.MinValue;
            if (DateTime.TryParse(dateTime, out time))
                return GetTimeStapmMilliseconds(time);
            else
                return 0;
        }

        /// <summary>
        /// 获取时间戳（毫秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStapmMilliseconds(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()).TotalMilliseconds;
        }

        /// <summary>
        /// 获取时间戳（秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStapmSeconds(string dateTime)
        {
            var time = DateTime.MinValue;
            if (DateTime.TryParse(dateTime, out time))
                return GetTimeStapmSeconds(time);
            else
                return 0;
        }

        /// <summary>
        /// 获取时间戳（秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStapmSeconds(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()).TotalSeconds;
        }



    }
}
