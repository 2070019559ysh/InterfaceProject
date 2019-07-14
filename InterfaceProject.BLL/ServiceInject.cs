using InterfaceProject.BLL.Handle;
using InterfaceProject.BLL.Manage;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceProject.BLL
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
            services.AddScoped<ISysConfigInfoManage, SysConfigInfoManage>();
            services.AddScoped<IJokeInfoManage, JokeInfoManage>();

            services.AddScoped<IWeChatHandle, WeChatHandle>();

            //DAL数据访问层的默认实现服务配置
            DAL.ServiceInject.ConfigureServices(services);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            SysConfigReader.Initialize(serviceProvider.GetService<ISysConfigInfoManage>());
        }
    }
}
