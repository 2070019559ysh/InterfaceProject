﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 个性化菜单的匹配规则，个性化菜单的更新有覆盖性，从新到旧进行匹配，直到成功匹配到一个菜单
    /// </summary>
    public class MatchRule
    {
        /// <summary>
        /// 用户标签的id，可通过用户标签管理接口获取
        /// </summary>
        public string tag_id;
        /// <summary>
        /// 性别：男（1）女（2），不填则不做匹配
        /// </summary>
        public int? sex;
        /// <summary>
        /// 客户端版本，当前只具体到系统型号：IOS(1), Android(2),Others(3)，不填则不做匹配
        /// </summary>
        public int? client_platform_type;
        /// <summary>
        /// 国家信息，是用户在微信中设置的地区【注意：country、province、city组成的地区信息从大到小验证】
        /// </summary>
        public string country;
        /// <summary>
        /// 省份信息，是用户在微信中设置的地区
        /// </summary>
        public string province;
        /// <summary>
        /// 城市信息，是用户在微信中设置的地区
        /// </summary>
        public string city;
        /// <summary>
        /// 语言信息，是用户在微信中设置的语言
        /// 1、简体中文 "zh_CN" 2、繁体中文TW "zh_TW" 3、繁体中文HK "zh_HK" 4、英文 "en" 5、印尼 "id" 6、马来 "ms" 7、西班牙 "es" 8、韩国 "ko" 9、意大利 "it" 10、日本 "ja" 11、波兰 "pl" 12、葡萄牙 "pt" 13、俄国 "ru" 14、泰文 "th" 15、越南 "vi" 16、阿拉伯语 "ar" 17、北印度 "hi" 18、希伯来 "he" 19、土耳其 "tr" 20、德语 "de" 21、法语 "fr"
        /// </summary>
        public string language;
    }
}
