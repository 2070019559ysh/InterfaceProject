using System;
using System.Text;

namespace InterfaceProject.Tool
{
    /// <summary>
    /// 异常对象的异常信息挖掘
    /// </summary>
    public static class ExceptionMsg
    {
        /// <summary>
        /// 获取异常对象包含的比较详细的异常信息
        /// </summary>
        /// <param name="exInfo">异常对象</param>
        /// <returns>异常信息</returns>
        public static string GetExceptionMsg(this Exception exInfo)
        {
            StringBuilder exMsgBuilder = new StringBuilder();
            int i = 0;
            while (exInfo != null)
            {
                var ex = exInfo;
                if (i == 0)
                {
                    exMsgBuilder.AppendFormat("\r\n异常信息：{0}", ex.Message);
                    exMsgBuilder.AppendFormat("\r\n异常堆栈：{0}", ex.StackTrace);
                }
                else
                {
                    exMsgBuilder.AppendFormat("\r\n内部异常信息：{0}", ex.Message);
                    exMsgBuilder.AppendFormat("\r\n内部异常堆栈：{0}", ex.StackTrace);
                }
                //var entityValidEx = ex as DbEntityValidationException;
                //if (entityValidEx != null)
                //{
                //    foreach (var entityValidationError in entityValidEx.EntityValidationErrors)
                //    {
                //        if (entityValidationError.IsValid == false)
                //        {
                //            foreach (var entityError in entityValidationError.ValidationErrors)
                //            {
                //                exMsgBuilder.AppendFormat("\r\n实体验证错误【{0}：{1}】", entityError.PropertyName, entityError.ErrorMessage);
                //            }
                //        }
                //    }
                //}
                exInfo = ex.InnerException;
                i++;
                if (i >= 3) break;
            }
            return exMsgBuilder.ToString();
        }
    }
}
