using System;
using InterfaceProject.DTO.Support;

namespace InterfaceProject.DTO.Support
{
    /// <summary>
    /// 封装含结果代号的结果数据类
    /// </summary>
    public class BsCodeResult
    {
        /// <summary>
        /// 平台返回响应码
        /// </summary>
        public ResultCode code;

        /// <summary>
        /// 平台返回响应说明
        /// </summary>
        public string desc;

        /// <summary>
        /// 实例化含代号的结果数据对象
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="desc">结果说明，可选</param>
        public BsCodeResult(ResultCode code, string desc = null)
        {
            this.code = code;
            this.desc = desc;
        }
    }

    /// <summary>
    /// 封装含结果代号的结果数据类
    /// </summary>
    public class BsCodeResult<T>
    {
        /// <summary>
        /// 平台返回响应码
        /// </summary>
        public ResultCode code;

        /// <summary>
        /// 平台返回响应说明
        /// </summary>
        public string desc;

        /// <summary>
        /// 处理结果数据
        /// </summary>
        public T dataResult;

        /// <summary>
        /// 实例化含代号的结果数据对象
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="desc">结果说明，可选</param>
        public BsCodeResult(ResultCode code, string desc = null)
        {
            this.code = code;
            this.desc = desc;
        }

        /// <summary>
        /// 实例化含代号的结果数据对象
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="desc">结果说明，可选</param>
        /// <param name="dataResult">结果数据</param>
        public BsCodeResult(ResultCode code, string desc, T dataResult) : this(code, desc)
        {
            this.dataResult = dataResult;
        }
    }
}