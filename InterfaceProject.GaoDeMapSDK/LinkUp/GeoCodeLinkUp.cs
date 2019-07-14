using InterfaceProject.GaoDeMapSDK.HelpModel;
using InterfaceProject.Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.GaoDeMapSDK.LinkUp
{
    /// <summary>
    /// 地理/逆地理编码
    /// </summary>
    public class GeoCodeLinkUp
    {
        /// <summary>
        /// 获取地理编码信息
        /// </summary>
        /// <param name="geoCodeParam">地理编码所需参数</param>
        /// <returns>地理编码信息</returns>
        public GeoCodeResult GetGeoCode(GeoCodeParam geoCodeParam)
        {
            string getUrl = "https://restapi.amap.com/v3/geocode/geo?{0}";
            string paras = QueryHelper.ToQueryString(geoCodeParam);
            getUrl = string.Format(getUrl, paras);
            GeoCodeResult geoCodeResult = SimulateRequest.HttpGet<GeoCodeResult>(getUrl);
            return geoCodeResult;
        }

        /// <summary>
        /// 获取逆地理编码信息
        /// </summary>
        /// <param name="reGeoCodeParam">逆地理编码所需参数</param>
        /// <returns>逆地理编码信息</returns>
        public ReGeoCodeResult GetReGeoCode(ReGeoCodeParam reGeoCodeParam)
        {
            string getUrl = "https://restapi.amap.com/v3/geocode/geo?{0}";
            string paras = QueryHelper.ToQueryString(reGeoCodeParam);
            getUrl = string.Format(getUrl, paras);
            ReGeoCodeResult reGeoCodeResult = SimulateRequest.HttpGet<ReGeoCodeResult>(getUrl);
            return reGeoCodeResult;
        }
    }
}
