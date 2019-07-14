using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InterfaceProject.NetOtherSDK.Weather.HelpModel
{
    /// <summary>
    /// 城市天气情况
    /// </summary>
    [XmlRoot("resp")]
    public class CityWeather
    {
        /// <summary>
        /// 城市名称
        /// </summary>
        [XmlElement("city")]
        public string City { get; set; }
        /// <summary>
        /// 更新的时间
        /// </summary>
        [XmlElement("updatetime")]
        public string UpdateTime { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        [XmlElement("wendu")]
        public decimal Wendu { get; set; }
        /// <summary>
        /// 风力
        /// </summary>
        [XmlElement("fengli")]
        public string Fengli { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        [XmlElement("shidu")]
        public string Shidu { get; set; }
        /// <summary>
        /// 风向
        /// </summary>
        [XmlElement("fengxiang")]
        public string Fengxiang { get; set; }
        /// <summary>
        /// 日出时间
        /// </summary>
        [XmlElement("sunrise_1")]
        public string Sunrise { get; set; }
        /// <summary>
        /// 日落时间
        /// </summary>
        [XmlElement("sunset_1")]
        public string Sunset { get; set; }
        /// <summary>
        /// 城市环境情况
        /// </summary>
        [XmlElement("environment")]
        public CityEnvironment CityEnvironment { get; set; }
        /// <summary>
        /// 城市昨天天气情况
        /// </summary>
        [XmlElement("yesterday")]
        public CityYesterday CityYesterday { get; set; }
        /// <summary>
        /// 预测未来天气
        /// </summary>
        [XmlArray("forecast")]
        [XmlArrayItem("weather")]
        public List<ForecastSky> Forecast { get; set; }
        /// <summary>
        /// 各个指数内容
        /// </summary>
        [XmlArray("zhishus")]
        [XmlArrayItem("zhishu")]
        public List<ZhiShu> ZhiShus { get; set; }
    }

    /// <summary>
    /// 城市环境情况
    /// </summary>
    [XmlRoot("environment")]
    public class CityEnvironment
    {
        /// <summary>
        /// Aqi
        /// </summary>
        [XmlElement("aqi")]
        public int Aqi { get; set; }
        /// <summary>
        /// PM 2.5
        /// </summary>
        [XmlElement("pm25")]
        public int PM25 { get; set; }
        /// <summary>
        /// 建议
        /// </summary>
        [XmlElement("suggest")]
        public string Suggest { get; set; }
        /// <summary>
        /// 空气质量
        /// </summary>
        [XmlElement("quality")]
        public string Quality { get; set; }
        /// <summary>
        /// 注意污染物
        /// </summary>
        [XmlElement("MajorPollutants")]
        public string MajorPollutants { get; set; }
        /// <summary>
        /// O3
        /// </summary>
        [XmlElement("o3")]
        public int O3 { get; set; }
        /// <summary>
        /// Co
        /// </summary>
        [XmlElement("Co")]
        public int Co { get; set; }
        /// <summary>
        /// PM 10
        /// </summary>
        [XmlElement("pm10")]
        public int PM10 { get; set; }
        /// <summary>
        /// So2
        /// </summary>
        [XmlElement("so2")]
        public int So2 { get; set; }
        /// <summary>
        /// No2
        /// </summary>
        [XmlElement("no2")]
        public int No2 { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        [XmlElement("time")]
        public string Time { get; set; }
    }

    /// <summary>
    /// 昨天天气情况
    /// </summary>
    [XmlRoot("yesterday")]
    public class CityYesterday
    {
        /// <summary>
        /// 日期
        /// </summary>
        [XmlElement("date_1")]
        public string Date { get; set; }
        /// <summary>
        /// 最高温度
        /// </summary>
        [XmlElement("high_1")]
        public string High { get; set; }
        /// <summary>
        /// 最低温度
        /// </summary>
        [XmlElement("low_1")]
        public string Low { get; set; }
        /// <summary>
        /// 白天天气情况
        /// </summary>
        [XmlElement("day_1")]
        public DateCd DayCondition { get; set; }//DateCondition
        /// <summary>
        /// 晚上天气情况
        /// </summary>
        [XmlElement("night_1")]
        public DateCd NightCondition { get; set; }
    }
    
    /// <summary>
    /// 当天日期天气情况
    /// </summary>
    public class DateCd
    {
        /// <summary>
        /// 天气类型
        /// </summary>
        [XmlElement("type_1")]
        public string SkyType { get; set; }
        /// <summary>
        /// 风向
        /// </summary>
        [XmlElement("fx_1")]
        public string Fengxiang { get; set; }
        /// <summary>
        /// 风力
        /// </summary>
        [XmlElement("fl_1")]
        public string Fengli { get; set; }
    }

    /// <summary>
    /// 当天日期天气情况
    /// </summary>
    public class DateCondition
    {
        /// <summary>
        /// 天气类型
        /// </summary>
        [XmlElement("type")]
        public string SkyType { get; set; }
        /// <summary>
        /// 风向
        /// </summary>
        [XmlElement("fengxiang")]
        public string Fengxiang { get; set; }
        /// <summary>
        /// 风力
        /// </summary>
        [XmlElement("fengli")]
        public string Fengli { get; set; }
    }

    /// <summary>
    /// 预测未来天气情况
    /// </summary>
    [XmlRoot("weather")]
    public class ForecastSky
    {
        /// <summary>
        /// 日期
        /// </summary>
        [XmlElement("date")]
        public string Date { get; set; }
        /// <summary>
        /// 最高温度
        /// </summary>
        [XmlElement("high")]
        public string High { get; set; }
        /// <summary>
        /// 最低温度
        /// </summary>
        [XmlElement("low")]
        public string Low { get; set; }
        /// <summary>
        /// 白天天气情况
        /// </summary>
        [XmlElement("day")]
        public DateCondition DayCondition { get; set; }
        /// <summary>
        /// 晚上天气情况
        /// </summary>
        [XmlElement("night")]
        public DateCondition NightCondition { get; set; }
    }

    /// <summary>
    /// 指数
    /// </summary>
    [XmlRoot("zhishu")]
    public class ZhiShu
    {
        /// <summary>
        /// 指数名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
        /// <summary>
        /// 指数值
        /// </summary>
        [XmlElement("value")]
        public string Value { get; set; }
        /// <summary>
        /// 指数值详细说明
        /// </summary>
        [XmlElement("detail")]
        public string Detail { get; set; }
    }
}
