using PublicTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBase.DLL
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public static class GlobleConfig
    {
        /// <summary>
        /// SQ链接字符串
        /// </summary>
        public readonly static string sqlcon = GetConfig.GetConfigs("MSSQLConnection:con0");
        public readonly static string webJsAdSetting = GetConfig.GetConfigs("RedisConnection:WebJsAdSetting");
        public readonly static string webJsFadSetting = GetConfig.GetConfigs("RedisConnection:WebJsFadSetting");
        public readonly static string webJsUiSetting = GetConfig.GetConfigs("RedisConnection:WebJsUiSetting");
        public readonly static string webJsZsSetting = GetConfig.GetConfigs("RedisConnection:WebJsZsSetting");
        /// <summary>
        /// IP统计标识
        /// </summary>
        public readonly static string IpsKey = GetConfig.GetConfigs("RedisConnection:Ipskey");
        /// <summary>
        /// 本站广告配置
        /// </summary>
        public readonly static string webrediskey = GetConfig.GetConfigs("RedisConnection:webcountfg");

        public readonly static string floatrediskey = GetConfig.GetConfigs("RedisConnection:floatcountfg");

        public readonly static string SumKey = GetConfig.GetConfigs("RedisConnection:SumKey");

        public readonly static string WebCarouselAdkey = GetConfig.GetConfigs("RedisConnection:WebCountAdID");
    }
}
