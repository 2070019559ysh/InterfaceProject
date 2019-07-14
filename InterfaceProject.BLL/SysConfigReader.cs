using InterfaceProject.BLL.Manage;
using InterfaceProject.Model.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.BLL
{
    public class SysConfigReader
    {
        private static ISysConfigInfoManage configInfoManage;

        /// <summary>
        /// 系统配置信息读取类的初始化
        /// </summary>
        /// <param name="configInfoManage">依赖系统配置数据Manage</param>
        public static void Initialize(ISysConfigInfoManage configInfoManage)
        {
            SysConfigReader.configInfoManage = configInfoManage;
        }

        /// <summary>
        /// 获取系统当前版本
        /// </summary>
        public static string Version
        {
            get
            {
                SysConfigInfo configInfo = configInfoManage.GetConfigInfo("Version");
                if (configInfo != null)
                {
                    string version = configInfo.Value;
                    return version;
                }
                return null;
            }
        }
    }
}
