using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfaceProject.BLL.Manage;
using InterfaceProject.DTO;
using InterfaceProject.DTO.Support;
using InterfaceProject.DTO.ViewModel;
using InterfaceProject.Model.CoreDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceProject.Web.Controllers.Api
{
    /// <summary>
    /// 笑话信息处理控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class JokeController : Controller
    {
        private readonly IJokeInfoManage jokeInfoManage;

        /// <summary>
        /// 实例化笑话信息处理控制器
        /// </summary>
        /// <param name="jokeInfoManage">依赖笑话数据管理对象</param>
        public JokeController(IJokeInfoManage jokeInfoManage)
        {
            this.jokeInfoManage = jokeInfoManage;
        }

        /// <summary>
        /// 按年月时间分页查询笑话信息
        /// </summary>
        /// <param name="ym">年月时间，为空则查全部</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页最大记录数</param>
        /// <returns>分页查询笑话集合</returns>
        [HttpGet("SearchJokeList")]
        public ResultEntity<DataPage<List<JokeInfo>>> SearchJokeList(string ym = null, int pageIndex = 1, int pageSize = 20)
        {
            DateTime yearMonth;
            DataPage<List<JokeInfo>> jokePageInfo;
            if (string.IsNullOrWhiteSpace(ym))
            {
                jokePageInfo = jokeInfoManage.SearchJokeInfos(null, pageIndex, pageSize);
            }
            else if(!DateTime.TryParse(ym,out yearMonth))
            {
                return new ResultEntity<DataPage<List<JokeInfo>>>(ResultCode.ParamWrong, "ym年月时间格式错误");
            }
            else
            {
                jokePageInfo = jokeInfoManage.SearchJokeInfos(yearMonth, pageIndex, pageSize);
            }
            return new ResultEntity<DataPage<List<JokeInfo>>>(ResultCode.SUCCESS, jokePageInfo);
        }
    }
}