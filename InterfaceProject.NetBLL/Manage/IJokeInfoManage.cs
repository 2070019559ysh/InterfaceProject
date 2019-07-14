using InterfaceProject.NetDTO.ManageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetBLL.Manage
{
    /// <summary>
    /// 笑话数据处理IManage接口
    /// </summary>
    public interface IJokeInfoManage
    {
        /// <summary>
        /// 保存笑话数据集合
        /// </summary>
        /// <param name="jokeInfoDtos">多个笑话数据的集合</param>
        /// <returns>保存成功的记录数</returns>
        int SaveRangeJokes(List<JokeInfoDto> jokeInfoDtos);
    }
}
