using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetOtherSDK.Weather.HelpModel
{
    /// <summary>
    /// 天气情况详情
    /// </summary>
    public class WeatherDetail
    {
        /// <summary>
        /// 昨天具体天气情况
        /// </summary>
        public YesterdaySky yesterday;
        /// <summary>
        /// 城市名称
        /// </summary>
        public string city;
        /// <summary>
        /// aqi
        /// </summary>
        public decimal aqi;
        /// <summary>
        /// 未来多天的天气预测
        /// </summary>
        public List<TodaySky> forecast;
        /// <summary>
        /// 感冒情况
        /// </summary>
        public string ganmao;
        /// <summary>
        /// 当前温度
        /// </summary>
        public decimal wendu;
    }

    /// <summary>
    /// 昨天天气情况
    /// </summary>
    public class YesterdaySky
    {
        /// <summary>
        /// 日期，如：16日星期二
        /// </summary>
        public string date;
        /// <summary>
        /// 最高温度，如：高温 26℃
        /// </summary>
        public string high;
        /// <summary>
        /// 风向，如：无持续风向
        /// </summary>
        public string fx;
        /// <summary>
        /// 最低温度，如：低温 21℃
        /// </summary>
        public string low;
        /// <summary>
        /// 风力，如：&lt;3级
        /// </summary>
        public string fl;
        /// <summary>
        /// 天气类型，如：阵雨
        /// </summary>
        public string type;
    }

    /// <summary>
    /// 今天天气情况
    /// </summary>
    public class TodaySky
    {
        /// <summary>
        /// 日期，如：21日星期天
        /// </summary>
        public string date;
        /// <summary>
        /// 最高温度，如：高温 28℃
        /// </summary>
        public string high;
        /// <summary>
        /// 风力，如：&lt;3级
        /// </summary>
        public string fengli;
        /// <summary>
        /// 最低温度，如：低温 23℃
        /// </summary>
        public string low;
        /// <summary>
        /// 风向，如：无持续风向
        /// </summary>
        public string fengxiang;
        /// <summary>
        /// 天气类型，如：多云
        /// </summary>
        public string type;
    }
}
