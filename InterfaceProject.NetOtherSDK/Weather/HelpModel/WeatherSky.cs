using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetOtherSDK.Weather.HelpModel
{
    /// <summary>
    /// 城市天空气象信息
    /// </summary>
    public class WeatherSky
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
        /// 温度
        /// </summary>
        public decimal Temperature;
        /// <summary>
        /// 风向
        /// </summary>
        public string WindDirection;
        /// <summary>
        /// 风级
        /// </summary>
        public string WindGrade;
        /// <summary>
        /// 湿度
        /// </summary>
        public string Humidity;
        /// <summary>
        /// 风力
        /// </summary>
        public string WindPower;
        /// <summary>
        /// 实况
        /// </summary>
        public string Condition;
        /// <summary>
        /// 风级英文
        /// </summary>
        public string WindGradeE;
        /// <summary>
        /// 有效时间
        /// </summary>
        public string Time;

        public decimal Sm;
        /// <summary>
        /// 是否启用雷达监测
        /// </summary>
        public bool IsRadar;
        /// <summary>
        /// 启用雷达编号
        /// </summary>
        public string Radar;
    }
}
