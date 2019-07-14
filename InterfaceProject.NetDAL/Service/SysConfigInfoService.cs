using InterfaceProject.NetModel.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetDAL.Service
{
    /// <summary>
    /// 系统配置信息处理Service
    /// </summary>
    public class SysConfigInfoService : ISysConfigInfoService
    {
        /// <summary>
        /// 根据Key来获取系统配置信息
        /// </summary>
        /// <param name="key">配置Key</param>
        /// <returns>系统配置信息</returns>
        public SysConfigInfo GetConfigInfo(string key)
        {
            using(var db = new InterfaceCoreDB())
            {
                SysConfigInfo configInfo = db.SysConfigInfo.Where(config => config.Key == key).FirstOrDefault();
                return configInfo;
            }
        }
    }
}
