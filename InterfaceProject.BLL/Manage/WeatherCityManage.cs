using InterfaceProject.DAL.Service;
using InterfaceProject.Model.CoreDB;
using InterfaceProject.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InterfaceProject.BLL.Manage
{
    /// <summary>
    /// 天气服务的城市信息保存Manage
    /// </summary>
    public class WeatherCityManage : IWeatherCityManage
    {
        private readonly IWeatherCityService weatherCityService;

        /// <summary>
        /// 实例化天气服务的城市信息保存Manage
        /// </summary>
        /// <param name="weatherCityService">依赖天气服务的城市信息保存IService</param>
        public WeatherCityManage(IWeatherCityService weatherCityService)
        {
            weatherCityService = this.weatherCityService;
        }

        /// <summary>
        /// 批量保存天气服务的城市信息
        /// </summary>
        /// <param name="connectionString">基本数据库连接字符串</param>
        /// <param name="weatherCities">天气服务的城市信息</param>
        public void SaveWeatherCity(string connectionString, List<WeatherCity> weatherCities)
        {
            if (weatherCities == null || weatherCities.Count == 0) return;
            DataTable wCityTable = DataTableHelper.ToDataTable(weatherCities);
            weatherCityService.SaveWeatherCity(connectionString, wCityTable);
        }

        /// <summary>
        /// 批量保存天气服务的城市信息
        /// </summary>
        /// <param name="weatherCities">天气服务的城市信息</param>
        /// <returns>成功保存记录数</returns>
        public int SaveWeatherCity(List<WeatherCity> weatherCities)
        {
            if (weatherCities == null || weatherCities.Count == 0) return 0;
            DataTable wCityTable = DataTableHelper.ToDataTable(weatherCities);
            int row = weatherCityService.SaveWeatherCity(wCityTable);
            return row;
        }
    }
}
