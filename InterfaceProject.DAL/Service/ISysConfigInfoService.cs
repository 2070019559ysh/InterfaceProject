using InterfaceProject.Model.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.DAL.Service
{
    /// <summary>
    /// 系统配置信息处理IService接口
    /// </summary>
    public interface ISysConfigInfoService
    {
        /// <summary>
        /// 根据Key来获取系统配置信息
        /// </summary>
        /// <param name="key">配置Key</param>
        /// <returns>系统配置信息</returns>
        SysConfigInfo GetConfigInfo(string key);
    }
}
