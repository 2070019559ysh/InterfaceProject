using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetTool
{
    /// <summary>
    /// 字符串附加工具类型
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 生成指定字符类型和个数的随机字符串
        /// </summary>
        /// <param name="strType">所需字符类型</param>
        /// <param name="n">字符个数</param>
        /// <returns></returns>
        public static string RandomStr(StrType strType,int n)//b：是否有复杂字符，n：生成的字符串长度
        {
            string numberStr = "0123456789";
            string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numberStrRemoveVague = "123456789";
            string lowerCaseRemoveVague = "abcdefghijkmnpqrstuvwxyz";
            string upperCaseRemoveVague = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            string specialCharacter = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";//复杂字符
            string str = string.Empty;
            switch (strType)
            {
                case StrType.Lowercase:str += lowerCase;break;
                case StrType.NumAndCase:str += numberStr + lowerCase + upperCase;break;
                case StrType.NumAndCaseRemoveVague:
                    str += numberStrRemoveVague + lowerCaseRemoveVague + upperCaseRemoveVague;break;
                case StrType.NumAndCaseSpecialCharacter:
                    str += numberStr + lowerCase + upperCase + specialCharacter;break;
                case StrType.NumAndLcaseRemoveVague:
                    str += numberStrRemoveVague + lowerCaseRemoveVague;break;
                case StrType.NumAndLowercase:
                    str += numberStr + lowerCase;break;
                case StrType.NumAndUcaseRemoveVague:
                    str += numberStrRemoveVague + upperCaseRemoveVague;break;
                case StrType.NumAndUppercase:str += numberStr + upperCase;break;
                case StrType.OnlyNumber:str += numberStr;break;
                case StrType.Uppercase:str+=upperCase;break;
                default:
                    str += numberStrRemoveVague + lowerCaseRemoveVague + upperCaseRemoveVague + specialCharacter;break;
            }
            StringBuilder randStr = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < n; i++)
            {
                randStr.Append(str.Substring(rd.Next(0, str.Length), 1));
            }
            return randStr.ToString();

        }
    }

    /// <summary>
    /// 所需字符串字符类型
    /// </summary>
    public enum StrType
    {
        /// <summary>
        /// 只是数字
        /// </summary>
        OnlyNumber=0,
        /// <summary>
        /// 只是小写字母
        /// </summary>
        Lowercase,
        /// <summary>
        /// 只是大写字母
        /// </summary>
        Uppercase,
        /// <summary>
        /// 数字和小写字母
        /// </summary>
        NumAndLowercase,
        /// <summary>
        /// 数字和大写字母
        /// </summary>
        NumAndUppercase,
        /// <summary>
        /// 数字和所有大、小写字母
        /// </summary>
        NumAndCase,
        /// <summary>
        /// 数字和所有大、小写字母，含特殊字符
        /// </summary>
        NumAndCaseSpecialCharacter,
        /// <summary>
        /// 数字和小写字母，去掉模糊字符，例如：1与l，0与o
        /// </summary>
        NumAndLcaseRemoveVague,
        /// <summary>
        /// 数字和大写字母，去掉模糊字符，例如：1与l，0与o
        /// </summary>
        NumAndUcaseRemoveVague,
        /// <summary>
        /// 数字和所有大、小写字母，去掉模糊字符，例如：1与l，0与o
        /// </summary>
        NumAndCaseRemoveVague
    }
}
