using Microsoft.Extensions.DependencyInjection;
using System;

namespace InterfaceProject.GaoDeMapSDK
{
    /// <summary>
    /// 依赖注入（Denpendency Injection)
    /// </summary>
    public class ServiceInject
    {
        /// <summary>
        /// 配置高德地图SDK所有依赖的服务
        /// </summary>
        /// <param name="services">依赖服务集合</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IConnectLinkUp, ConnectLinkUp>();
        }
    }
}
