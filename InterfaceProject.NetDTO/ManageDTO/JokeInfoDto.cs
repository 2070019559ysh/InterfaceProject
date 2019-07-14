using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetDTO.ManageDTO
{
    /// <summary>
    /// 封装笑话信息DTO
    /// </summary>
    public class JokeInfoDto
    {
        /// <summary>
        /// 笑话内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 唯一ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// UNIX创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
