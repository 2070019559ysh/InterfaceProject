using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.DTO
{
    /// <summary>
    /// 说明字段的特性
    /// </summary>
    internal class ExplainAttribute:Attribute
    {
        /// <summary>
        /// 说明内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 指定字段的说明内容
        /// </summary>
        /// <param name="content">说明内容</param>
        public ExplainAttribute(string content)
        {
            Content = content;
        }
    }

    /// <summary>
    /// 记录字段信息
    /// </summary>
    /// <typeparam name="T">字段值的类型</typeparam>
    internal class FieldDesc<T>
    {
        /// <summary>
        /// 字段的名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段的值
        /// </summary>
        public T FieldValue { get; set; }
        /// <summary>
        /// 字段的说明内容
        /// </summary>
        public string ExplainContent { get; set; }
    }
}
