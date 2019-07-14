using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.DTO.StatusConst
{
    /// <summary>
    /// 记录天气城市等级
    /// </summary>
    public class WCityLevel
    {
        /// <summary>
        /// 省
        /// </summary>
        public const int PROVINCE = 1;
        /// <summary>
        /// 市
        /// </summary>
        public const int CITY = 2;
        /// <summary>
        /// 区
        /// </summary>
        public const int REGION = 3;

        /// <summary>
        /// 存储的Key--Value
        /// </summary>
        public static readonly Dictionary<int, string> keyValuePairs = new Dictionary<int, string>
        {
            { PROVINCE,"省份" },
            { CITY,"城市" },
            { REGION,"区域" }
        };

        /// <summary>
        /// 获取中文名称
        /// </summary>
        /// <param name="cityLevel">天气城市等级</param>
        /// <returns></returns>
        public static string GetName(int cityLevel)
        {
            if (keyValuePairs.ContainsKey(cityLevel))
                return keyValuePairs[cityLevel];
            else
                return "未知";
        }

        /// <summary>
        /// 判断指定的天气城市等级是否有效
        /// </summary>
        /// <param name="cityLevel">天气城市等级</param>
        /// <returns>是否有效</returns>
        public static bool IsValid(int cityLevel)
        {
            return keyValuePairs.ContainsKey(cityLevel);
        }
    }
}
