using InterfaceProject.DAL.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.DAL
{
    /// <summary>
    /// 类库默认实现依赖服务注入
    /// </summary>
    public class ServiceInject
    {
        /// <summary>
        /// 进行默认实现的依赖服务配置
        /// </summary>
        /// <param name="services">提供服务注入的集合</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISysConfigInfoService, SysConfigInfoService>();
            services.AddScoped<IJokeInfoService, JokeInfoService>();
            services.AddScoped<IWeatherCityService, WeatherCityService>();
        }
    }
}
