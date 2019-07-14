using InterfaceProject.NetBLL.Manage;
using InterfaceProject.NetModel.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetBLL
{
    public class SysConfigReader
    {
        private static readonly ISysConfigInfoManage configInfoManage;

        static SysConfigReader()
        {
            configInfoManage = new SysConfigInfoManage();
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
