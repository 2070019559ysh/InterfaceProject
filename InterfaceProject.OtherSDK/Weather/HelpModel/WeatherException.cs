using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.OtherSDK.Weather.HelpModel
{
    /// <summary>
    /// 天气信息请求异常
    /// </summary>
    public class WeatherException:Exception
    {
        /// <summary>
        /// 异常错误代号
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 异常错误具体说明
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 实例化个天气信息请求异常
        /// </summary>
        /// <param name="errCode">异常错误代号</param>
        /// <param name="errMsg">异常错误具体说明</param>
        public WeatherException(int errCode,string errMsg)
        {
            ErrorCode = errCode;
            ErrorMsg = errMsg;
        }
    }
}
