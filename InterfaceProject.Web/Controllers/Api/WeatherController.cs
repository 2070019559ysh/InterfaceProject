using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfaceProject.DTO;
using InterfaceProject.OtherSDK.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InterfaceProject.OtherSDK.Weather.HelpModel;
using Microsoft.Extensions.Logging;

namespace InterfaceProject.Web.Controllers.Api
{
    /// <summary>
    /// 提供城市代号和天气查询服务
    /// </summary>
    [Produces("application/json")]
    [Route("api/Weather")]
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> logger;
        private readonly IAreaProvider areaProvider;
        private readonly IWeatherProvider weatherProvider;

        /// <summary>
        /// 实例化天气查询服务控制器
        /// </summary>
        /// <param name="areaProvider"></param>
        public WeatherController(ILogger<WeatherController> logger,IAreaProvider areaProvider, IWeatherProvider weatherProvider)
        {
            this.logger = logger;
            this.areaProvider = areaProvider;
            this.weatherProvider = weatherProvider;
        }

        /// <summary>
        /// 获取指定等级区域代号下的子区域信息集合
        /// </summary>
        /// <param name="level">需获取等级：1=省，2=市，3=区</param>
        /// <param name="areaCode">父区域代号</param>
        /// <returns>子区域代号集合</returns>
        [HttpGet("GetAreaCode")]
        public ResultEntity<List<AreaCodeName>> GetAreaCode(int level = 1, string areaCode = "")
        {
            AreaLevel areaLevel;
            try
            {
                areaLevel = (AreaLevel)level;
            }
            catch (Exception)
            {
                return new ResultEntity<List<AreaCodeName>>(ResultCode.ParamWrong, "level区域等级参数错误");
            }
            List<AreaCodeName> areaCodeList = areaProvider.SearchAreaCode(areaLevel, areaCode);
            if (areaCodeList != null)
                return new ResultEntity<List<AreaCodeName>>(ResultCode.SUCCESS, areaCodeList);
            else
                return new ResultEntity<List<AreaCodeName>>(ResultCode.Failure, "加载区域代号失败");
        }

        /// <summary>
        /// 获取城市天气信息
        /// </summary>
        /// <param name="cityId">城市的区域代号</param>
        /// <returns>城市天气信息</returns>
        [HttpGet("GetCityWeather")]
        public ResultEntity<CityWeather> GetCityWeather(string cityId)
        {
            try
            {
                CityWeather cityWeather = weatherProvider.GetCityWeather(cityId);
                return new ResultEntity<CityWeather>(ResultCode.SUCCESS, cityWeather);
            }
            catch (WeatherException ex)
            {
                logger.LogError(new EventId(ex.ErrorCode, "加载城市天气信息"), "远程加载城市天气信息异常", ex);
                return new ResultEntity<CityWeather>(ResultCode.RemoteRequestFail, ex.ErrorMsg);
            }
        }
    }
}