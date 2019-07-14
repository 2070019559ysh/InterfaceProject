using InterfaceProject.OtherSDK.Weather.HelpModel;
using InterfaceProject.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InterfaceProject.OtherSDK.Weather
{
    /// <summary>
    /// 天气信息提供服务
    /// </summary>
    public class WeatherProvider : IWeatherProvider
    {
        /// <summary>
        /// 获取城市天空气象信息
        /// </summary>
        /// <returns>返回天空气象信息，参数无法解析会返回null</returns>
        public WeatherSky GetWeatherSky(string cityId)
        {
            string getUrl = string.Format("http://www.weather.com.cn/data/sk/{0}.html", cityId);
            JObject weatherResult = SimulateRequest.HttpGet<JObject>(getUrl);
            string weatherStr = weatherResult["weatherinfo"]?.ToString();
            if (!string.IsNullOrWhiteSpace(weatherStr))
            {
                SkyClimate skyClimate = JsonConvert.DeserializeObject<SkyClimate>(weatherStr);
                if (skyClimate != null)
                {
                    WeatherSky weatherSky = new WeatherSky()
                    {
                        CityId = skyClimate.cityid,
                        CityName = skyClimate.city,
                        Condition = skyClimate.njd,
                        Humidity = skyClimate.SD,
                        Sm = skyClimate.sm,
                        Temperature = skyClimate.temp,
                        Time = skyClimate.time,
                        WindDirection = skyClimate.WD,
                        WindGrade = skyClimate.WS,
                        WindPower = skyClimate.AP,
                        WindGradeE = skyClimate.WSE,
                        IsRadar = skyClimate.isRadar,
                        Radar = skyClimate.Radar
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// 获取城市天气信息
        /// </summary>
        /// <param name="cityId">天气城市代号</param>
        /// <returns>返回城市天气信息，参数无法解析会返回null</returns>
        public WeatherInfo GetWeatherInfo(string cityId)
        {
            string getUrl = string.Format("http://www.weather.com.cn/data/cityinfo/{0}.html", cityId);
            JObject weatherResult = SimulateRequest.HttpGet<JObject>(getUrl);
            string weatherStr = weatherResult["weatherinfo"]?.ToString();
            if (!string.IsNullOrWhiteSpace(weatherStr))
            {
                ClimateInfo climate = JsonConvert.DeserializeObject<ClimateInfo>(weatherStr);
                if (climate != null)
                {
                    WeatherInfo weatherSky = new WeatherInfo()
                    {
                        CityId = climate.cityid,
                        CityName = climate.city,
                        TempMin = climate.temp1,
                        TempMax = climate.temp2,
                        WeatherDesc = climate.weather,
                        WindImg1 = climate.img1,
                        WindImg2 = climate.img2,
                        Time = climate.ptime
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// 获取城市天气详情信息
        /// </summary>
        /// <param name="cityId">天气城市代号</param>
        /// <returns>城市天气详情信息</returns>
        public WeatherDetail GetWeatherDetail(string cityId)
        {
            string getUrl = string.Format("http://wthrcdn.etouch.cn/weather_mini?citykey={0}", cityId);
            JObject weatherResult = SimulateRequest.HttpGet<JObject>(getUrl);
            string statusStr = weatherResult["status"]?.ToString();
            int.TryParse(statusStr, out int status);
            if (1000 == status)
            {
                string weatherStr = weatherResult["data"]?.ToString();
                if (!string.IsNullOrWhiteSpace(weatherStr))
                {
                    WeatherDetail weatherDetail = JsonConvert.DeserializeObject<WeatherDetail>(weatherStr);
                    if (weatherDetail != null)
                    {
                        if (!string.IsNullOrWhiteSpace(weatherDetail?.yesterday?.fl))
                            weatherDetail.yesterday.fl = weatherDetail.yesterday.fl.Replace("<![CDATA[", "").Replace("]]", "");
                        if (weatherDetail.forecast != null)
                        {
                            weatherDetail.forecast.ForEach(todaySky =>
                            {
                                if (!string.IsNullOrWhiteSpace(todaySky?.fengli))
                                    todaySky.fengli = todaySky.fengli.Replace("<![CDATA[", "").Replace("]]", "");
                            });
                        }
                    }
                }
            }
            else
            {
                throw new WeatherException(status, weatherResult["desc"]?.ToString());
            }
            return null;
        }

        /// <summary>
        /// 获取城市天气详情信息
        /// </summary>
        /// <param name="cityId">天气城市代号</param>
        /// <returns>城市天气详情信息</returns>
        public CityWeather GetCityWeather(string cityId)
        {
            string getUrl = string.Format("http://wthrcdn.etouch.cn/WeatherApi?citykey={0}", cityId);
            string weatherXml = SimulateRequest.HttpClientGet(getUrl, true);
            if (string.IsNullOrWhiteSpace(weatherXml))
                throw new WeatherException(-200, "请求到的天气数据Xml为空！");
            CityWeather cityWeather = XmlConvert.DeserializeObject<CityWeather>(weatherXml);
            return cityWeather;
        }
    }

    /// <summary>
    /// 城市天空气象信息
    /// </summary>
    internal class SkyClimate
    {
        /// <summary>
        /// 城市Id
        /// </summary>
        internal string cityid;
        /// <summary>
        /// 城市名称
        /// </summary>
        internal string city;
        /// <summary>
        /// 温度
        /// </summary>
        internal decimal temp;
        /// <summary>
        /// 风向
        /// </summary>
        internal string WD;
        /// <summary>
        /// 风级
        /// </summary>
        internal string WS;
        /// <summary>
        /// 湿度
        /// </summary>
        internal string SD;
        /// <summary>
        /// 风力
        /// </summary>
        internal string AP;
        /// <summary>
        /// 实况
        /// </summary>
        internal string njd;
        /// <summary>
        /// 风级英文
        /// </summary>
        internal string WSE;
        /// <summary>
        /// 有效时间
        /// </summary>
        internal string time;

        internal decimal sm;
        /// <summary>
        /// 是否启用雷达监测
        /// </summary>
        internal bool isRadar;
        /// <summary>
        /// 启用雷达编号
        /// </summary>
        internal string Radar;
    }

    /// <summary>
    /// 城市天气信息
    /// </summary>
    internal class ClimateInfo
    {
        /// <summary>
        /// 城市Id
        /// </summary>
        internal string cityid;
        /// <summary>
        /// 城市名称
        /// </summary>
        internal string city;
        /// <summary>
        /// 最低温度
        /// </summary>
        internal string temp1;
        /// <summary>
        /// 最高温度
        /// </summary>
        internal string temp2;
        /// <summary>
        /// 气候
        /// </summary>
        internal string weather;
        /// <summary>
        /// 气候图片1
        /// </summary>
        internal string img1;
        /// <summary>
        /// 气候图片2
        /// </summary>
        internal string img2;
        /// <summary>
        /// 有效时间
        /// </summary>
        internal string ptime;
    }
}
