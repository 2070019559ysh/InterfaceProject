using InterfaceProject.WxSDK;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDKTest
{
    /// <summary>
    /// 注册依赖服务中所有测试的基类
    /// </summary>
    [TestClass]
    public class ServiceInjectTestBase
    {
        public IServiceProvider serviceProvider;

        /// <summary>
        /// 初始化时注册依赖服务
        /// </summary>
        public ServiceInjectTestBase()
        {
            var serviceCollection = new ServiceCollection();
            ServiceInject.ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
