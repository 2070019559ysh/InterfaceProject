using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.OtherSDK.JuHe.HelpModel
{
    /// <summary>
    /// 封装笑话信息
    /// </summary>
    public class JokeInfo
    {
        /// <summary>
        /// 笑话内容
        /// </summary>
        public string content;
        /// <summary>
        /// 唯一ID
        /// </summary>
        public Guid hashId;
        /// <summary>
        /// UNIX创建时间
        /// </summary>
        public long unixtime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updatetime;
    }

    /// <summary>
    /// 封装多个笑话的集合对象
    /// </summary>
    public class JokesData
    {
        public List<JokeInfo> data;
    }
}
