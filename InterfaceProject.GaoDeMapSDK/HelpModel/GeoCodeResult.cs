using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace InterfaceProject.GaoDeMapSDK.HelpModel
{
    /// <summary>
    /// 地理编码信息结果对象
    /// </summary>
    [XmlRoot("response")]
    public class GeoCodeResult
    {
        /// <summary>
        /// 结果状态值：返回值为 0 或 1，0 表示请求失败；1 表示请求成功
        /// </summary>
        [XmlElement("status")]
        public int status;
        /// <summary>
        /// 返回结果数目
        /// </summary>
        [XmlElement("count")]
        public int count;
        /// <summary>
        /// 返回状态说明：当 status 为 0 时，info 会返回具体错误原因，否则返回“OK”
        /// </summary>
        [XmlElement("info")]
        public string info;
        /// <summary>
        /// 返回状态码
        /// </summary>
        [XmlElement("infocode")]
        public string infocode;
        /// <summary>
        /// 地理编码信息列表
        /// </summary>
        [XmlArray("geocodes")]
        [XmlArrayItem("geocode")]
        public List<GeoCodeInfo> geocodes;
    }

    /// <summary>
    /// 地理编码信息
    /// </summary>
    [XmlRoot("geocode")]
    public class GeoCodeInfo
    {
        /// <summary>
        /// 结构化地址信息，格式：省份＋城市＋区县＋城镇＋乡村＋街道＋门牌号码
        /// </summary>
        [XmlElement("formatted_address")]
        public string formatted_address;
        /// <summary>
        /// 地址所在的国家
        /// </summary>
        [XmlElement("country")]
        public string country;
        /// <summary>
        /// 地址所在的省份名
        /// </summary>
        [XmlElement("province")]
        public string province;
        /// <summary>
        /// 地址所在的城市名
        /// </summary>
        [XmlElement("city")]
        public string city;
        /// <summary>
        /// 城市编码
        /// </summary>
        [XmlElement("citycode")]
        public string citycode;
        /// <summary>
        /// 地址所在的区
        /// </summary>
        [XmlElement("district")]
        public string district;
        /// <summary>
        /// 地址所在的乡镇
        /// </summary>
        [XmlElement("township")]
        public string township;
        /// <summary>
        /// 地址所在的街道
        /// </summary>
        [XmlElement("street")]
        public string street;
        /// <summary>
        /// 地址所在的门牌
        /// </summary>
        [XmlElement("number")]
        public string number;
        /// <summary>
        /// 区域编码
        /// </summary>
        [XmlElement("adcode")]
        public string adcode;
        /// <summary>
        /// 坐标点：经度，纬度
        /// </summary>
        [XmlElement("location")]
        public string location;
        /// <summary>
        /// 匹配级别
        /// </summary>
        [XmlElement("level")]
        public string level;
    }
}
