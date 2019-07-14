using InterfaceProject.NetOtherSDK.Weather.HelpModel;
using InterfaceProject.NetTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetOtherSDK.Weather
{
    /// <summary>
    /// 天气服务提供的区域信息的提供器
    /// </summary>
    public class AreaProvider: IAreaProvider
    {
        /// <summary>
        /// 获取天气服务提供的区域信息
        /// </summary>
        /// <param name="level">区域等级</param>
        /// <param name="areaCode">区域代号，当level=1时请默认留空</param>
        /// <returns>区域信息</returns>
        public List<AreaCodeName> SearchAreaCode(AreaLevel level,string areaCode = "")
        {
            string getUrl = string.Format("http://www.weather.com.cn/data/list3/city{0}.xml?level={1}", areaCode, (int)level);
            string areaStr = SimulateRequest.HttpGet(getUrl);
            string[] areaResultAry = areaStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<AreaCodeName> areaInfoList = areaResultAry.Select(areaResult =>
            {
                string[] areaAry = areaResult.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                return new AreaCodeName
                {
                    Code = areaAry[0],
                    Name = areaAry[1]
                };
            }).ToList();
            return areaInfoList;
        }
    }
}
