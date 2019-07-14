using InterfaceProject.DTO.ViewModel;
using InterfaceProject.Model.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.BLL.Manage
{
    /// <summary>
    /// 笑话数据处理IManage接口
    /// </summary>
    public interface IJokeInfoManage
    {
        /// <summary>
        /// 按月份分页查找最新笑话集合
        /// </summary>
        /// <param name="yearMonth">可查询指定年月份的数量</param>
        /// <param name="pageIndex">当前页码，默认1开始</param>
        /// <param name="pageSize">指定每页最大记录数</param>
        /// <returns>笑话总记录数和数据集合</returns>
        DataPage<List<JokeInfo>> SearchJokeInfos(DateTime? yearMonth, int pageIndex = 1, int pageSize = 20);
    }
}
