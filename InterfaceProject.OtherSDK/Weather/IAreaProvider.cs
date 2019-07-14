using InterfaceProject.OtherSDK.Weather.HelpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.OtherSDK.Weather
{
    /// <summary>
    /// 天气服务提供的区域信息的提供器接口
    /// </summary>
    public interface IAreaProvider
    {
        /// <summary>
        /// 获取天气服务提供的区域信息
        /// </summary>
        /// <param name="level">区域等级</param>
        /// <param name="areaCode">区域代号，当level=1时请默认留空</param>
        /// <returns>区域信息</returns>
        List<AreaCodeName> SearchAreaCode(AreaLevel level, string areaCode = "");
    }
}
