using InterfaceProject.NetOtherSDK.Weather;
using InterfaceProject.WinForm.InnerTool;
using InterfaceProject.NetOtherSDK.Weather.HelpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceProject.NetBLL.Manage;
using InterfaceProject.NetModel.CoreDB;
using InterfaceProject.NetDTO.StatusConst;

namespace InterfaceProject.WinForm.TimerTask
{
    /// <summary>
    /// 存储天气的区域信息定时任务
    /// </summary>
    public class WeatherAreaTimerTask : AbstractTimerTask
    {
        private readonly IAreaProvider areaProvider;
        private readonly IWeatherCityManage weatherCityManage;

        public WeatherAreaTimerTask()
        {
            areaProvider = new AreaProvider();
            weatherCityManage = new WeatherCityManage();
        }

        /// <summary>
        /// 执行定时任务，保存日志到库
        /// </summary>
        /// <param name="richTextLog">winform日志显示</param>
        public override void ExecMethod(RichTextConsole console)
        {
            console.Debug($"{TimerName}定时任务，开始执行...", false);
            List<AreaCodeName> areaCodeNameList = areaProvider.SearchAreaCode(AreaLevel.Province);
            List<WeatherCity> weatherCities = new List<WeatherCity>();
            areaCodeNameList.ForEach(areaCodeName =>
            {
                WeatherCity weatherCity = new WeatherCity()
                {
                    Id = areaCodeName.Code,
                    Name = areaCodeName.Name,
                    ParentId = "null",
                    WCityLevel = WCityLevel.PROVINCE,
                    WCityLevelName = WCityLevel.GetName(WCityLevel.PROVINCE),
                    CreateTime = DateTime.Now
                };
                weatherCities.Add(weatherCity);
            });
            weatherCityManage.SaveWeatherCity(WinFormConfig.CoreDBConnectString, weatherCities);
            console.Info($"{TimerName}定时任务，成功保存省份集合信息。", isWriteLog: false);
            weatherCities.Clear();//清空省份数据，开始保存城市数据
            areaCodeNameList.ForEach(areaCodeName =>
            {
                List<AreaCodeName> cityAreaCodeNameList = areaProvider.SearchAreaCode(AreaLevel.City,areaCodeName.Code);
                for(int i = 0; i < cityAreaCodeNameList.Count; i++)
                {
                    var cityAreaCodeName = cityAreaCodeNameList[i];
                    WeatherCity weatherCity = new WeatherCity()
                    {
                        Id = cityAreaCodeName.Code,
                        Name = cityAreaCodeName.Name,
                        ParentId = areaCodeName.Code,
                        WCityLevel = WCityLevel.CITY,
                        WCityLevelName = WCityLevel.GetName(WCityLevel.CITY),
                        CreateTime = DateTime.Now
                    };
                    weatherCities.Add(weatherCity);
                }
                weatherCityManage.SaveWeatherCity(WinFormConfig.CoreDBConnectString, weatherCities);
                console.Info($"{TimerName}定时任务，成功保存{areaCodeName.Name}省份的城市集合信息。", isWriteLog: false);
                weatherCities.Clear();//清空城市数据，开始保存城市的区域数据
                cityAreaCodeNameList.ForEach(cityCodeName=>
                {
                    List<AreaCodeName> regionCodeNameList = areaProvider.SearchAreaCode(AreaLevel.Region, cityCodeName.Code);
                    foreach(var regionCodeName in regionCodeNameList)
                    {
                        WeatherCity weatherCity = new WeatherCity()
                        {
                            Id = regionCodeName.Code,
                            Name = regionCodeName.Name,
                            ParentId = cityCodeName.Code,
                            WCityLevel = WCityLevel.REGION,
                            WCityLevelName = WCityLevel.GetName(WCityLevel.REGION),
                            CreateTime = DateTime.Now
                        };
                        weatherCities.Add(weatherCity);
                    }
                    weatherCityManage.SaveWeatherCity(WinFormConfig.CoreDBConnectString, weatherCities);
                    console.Info($"{TimerName}定时任务，成功保存{cityCodeName.Name}城市的区域集合信息。", isWriteLog: false);
                    weatherCities.Clear();//清空区域数据，开始保存下一个城市的区域数据
                });
                weatherCities.Clear();//清空城市数据，开始保存下一个省份的城市数据
            });
            console.Info($"{TimerName}定时任务，已执行完成。", isWriteLog: false);
        }
    }
}
