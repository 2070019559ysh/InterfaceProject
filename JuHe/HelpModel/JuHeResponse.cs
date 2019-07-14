using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.OtherSDK.JuHe.HelpModel
{
    /// <summary>
    /// 封装统一的聚合平台返回接口数据
    /// </summary>
    /// <typeparam name="T">指定数据类型</typeparam>
    public class JuHeResponse<T>
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int error_code;
        /// <summary>
        /// 错误原因
        /// </summary>
        public string reason;
        /// <summary>
        /// 响应结果对象
        /// </summary>
        public object result;

        /// <summary>
        /// 获取响应结果对象的具体类型数据
        /// </summary>
        /// <returns>响应结果对象的具体类型数据</returns>
        public T GetResult()
        {
            try
            {
                string resultStr = result as string;
                if (resultStr == null)
                {
                    resultStr = JsonConvert.SerializeObject(result);
                }
                //如果结果字符串符合json
                T resultData = JsonConvert.DeserializeObject<T>(resultStr);
                return resultData;
            }
            catch (Exception)
            {
                //结果字符串符合不符合json，需要转具体的值类型，如：string, bool, int, decimal
                T resultData = (T)Convert.ChangeType(result, typeof(T));
                return resultData;
            }
        }
    }
}
