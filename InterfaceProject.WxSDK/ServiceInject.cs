using InterfaceProject.WxSDK.LinkUp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK
{
    /// <summary>
    /// 依赖注入（Denpendency Injection)
    /// </summary>
    public class ServiceInject
    {
        /// <summary>
        /// 配置微信SDK所有依赖的服务
        /// </summary>
        /// <param name="services">依赖服务集合</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IConnectLinkUp, ConnectLinkUp>();
            services.AddScoped<ICustomMenuLinkUp, CustomMenuLinkUp>();
            services.AddScoped<IMessageLinkUp, MessageLinkUp>();
            services.AddScoped<ICustomerServiceMsgLinkUp, CustomerServiceMsgLinkUp>();
            services.AddScoped<IGroupSendLinkUp, GroupSendLinkUp>();
            services.AddScoped<IOnceSubscribeLinkUp, OnceSubscribeLinkUp>();
            services.AddScoped<IMaterialLinkUp, MaterialLinkUp>();
        }
    }
}
