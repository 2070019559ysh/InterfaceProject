using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.OtherSDK.Weather.HelpModel
{
    /// <summary>
    /// 城市天气信息
    /// </summary>
    public class WeatherInfo
    {
        /// <summary>
        /// 城市Id
        /// </summary>
        public string CityId;
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName;
        /// <summary>
        /// 最低温度
        /// </summary>
        public string TempMin;
        /// <summary>
        /// 最高温度
        /// </summary>
        public string TempMax;
        /// <summary>
        /// 气候
        /// </summary>
        public string WeatherDesc;
        /// <summary>
        /// 气候图片1
        /// </summary>
        public string WindImg1;
        /// <summary>
        /// 气候图片2
        /// </summary>
        public string WindImg2;
        /// <summary>
        /// 有效时间
        /// </summary>
        public string Time;
    }
}
