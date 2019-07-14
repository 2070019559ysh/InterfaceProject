using InterfaceProject.OtherSDK.Weather.HelpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.OtherSDK.Weather
{
    /// <summary>
    /// 天气信息提供服务接口声明
    /// </summary>
    public interface IWeatherProvider
    {
        /// <summary>
        /// 获取城市天空气象信息
        /// </summary>
        /// <returns>返回天空气象信息，参数无法解析会返回null</returns>
        WeatherSky GetWeatherSky(string cityId);

        /// <summary>
        /// 获取城市天气信息
        /// </summary>
        /// <param name="cityId">天气城市代号</param>
        /// <returns>返回城市天气信息，参数无法解析会返回null</returns>
        WeatherInfo GetWeatherInfo(string cityId);

        /// <summary>
        /// 获取城市天气详情信息
        /// </summary>
        /// <param name="cityId">天气城市代号</param>
        /// <returns>城市天气详情信息</returns>
        WeatherDetail GetWeatherDetail(string cityId);

        /// <summary>
        /// 获取城市天气详情信息
        /// </summary>
        /// <param name="cityId">天气城市代号</param>
        /// <returns>城市天气详情信息</returns>
        CityWeather GetCityWeather(string cityId);
    }
}
