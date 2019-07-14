using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.DTO.ViewModel
{
    /// <summary>
    /// 分页数据封装类
    /// </summary>
    /// <typeparam name="T">指定数据集合的类型</typeparam>
    public class DataPage<T> where T : class
    {
        /// <summary>
        /// 运营后台DataTable分页的数据集合
        /// </summary>
        public T aaData { get; set; }
        /// <summary>
        /// 库中的总数
        /// </summary>
        public int iTotalDisplayRecords { get; set; }
        /// <summary>
        /// 每页显示最大记录数
        /// </summary>
        public int iTotalRecords { get; set; }
    }
}
