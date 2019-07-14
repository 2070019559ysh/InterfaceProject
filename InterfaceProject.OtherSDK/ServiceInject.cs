using InterfaceProject.OtherSDK.Weather;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.OtherSDK
{
    /// <summary>
    /// 依赖注入（Denpendency Injection)
    /// </summary>
    public class ServiceInject
    {
        /// <summary>
        /// 配置其他SDK所有依赖的服务
        /// </summary>
        /// <param name="services">依赖服务集合</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAreaProvider, AreaProvider>();
            services.AddScoped<IWeatherProvider, WeatherProvider>();
        }
    }
}
