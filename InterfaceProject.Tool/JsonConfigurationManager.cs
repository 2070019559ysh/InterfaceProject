using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InterfaceProject.Tool
{
    /// <summary>
    /// 读取appsettings.json的配置
    /// </summary>
    public static class JsonConfigurationManager
    {
        /// <summary>
        /// 获取appsettings.json的配置的值
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="key">json配置的key</param>
        /// <returns>解析号的配置对象</returns>
        public static T GetAppSettings<T>(string key) where T : class, new()
        {
            var baseDir = AppContext.BaseDirectory;
            var indexSrc = baseDir.IndexOf("src");
            string currentDir = string.Empty;
            if (indexSrc > 0)
            {
                currentDir = baseDir.Substring(0, indexSrc);
            }
            else if ((indexSrc = baseDir.IndexOf("bin")) > 0)
            {
                currentDir = baseDir.Substring(0, indexSrc);
            }
            else
            {
                currentDir = baseDir;
            }
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(currentDir)
                .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
                .Build();
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }
    }
}
