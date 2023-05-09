using System;
using Microsoft.Extensions.Configuration;

namespace PublicTools
{
    public class GetConfig
    {

        /// <summary>
        /// 获取配置文件中的内容，继承自IConfiguration
        /// </summary>
        private static IConfiguration _configuration { get; set; }

        /// <summary>
        /// 获取到配置文件
        /// </summary>
        /// <param name="fileName"></param>
        public GetConfig(string fileName)
        {
            //在当前目录或者根目录中寻找appsettings.json文件

            var directory = AppContext.BaseDirectory;
            directory = directory.Replace("\\", "/");

            var filePath = $"{directory}/{fileName}";

            var builder = new ConfigurationBuilder()
                .AddJsonFile(filePath, false, true);

            _configuration = builder.Build();
        }

        /// <summary>
        /// 获取到配置文件
        /// </summary>
        /// <param name="configName"></param>
        public string GetCon(string configName)
        {
            return _configuration[configName];
        }

        /// <summary>
        /// 获取配置文件数据，获取格式："A：B：C"
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConfigs(string configName)
        {
            return _configuration[configName];
        }




    }
}
