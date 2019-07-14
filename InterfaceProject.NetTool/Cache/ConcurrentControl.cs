using InterfaceProject.NetTool.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetTool.Cache
{
    /// <summary>
    /// 控制高并发下能安全执行的封装类
    /// </summary>
    public class ConcurrentControl
    {
        private static RedisHelper redisHelper = new RedisHelper();

        /// <summary>
        /// 控制单用户并发来封装执行函数
        /// </summary>
        /// <typeparam name="T">执行函数的返回类型</typeparam>
        /// <param name="concurrentKey">控制单用户并发的RedisKey</param>
        /// <param name="concurrentResult">发生单用户并发时返回结果</param>
        /// <param name="execFunc">单用户访问的执行函数</param>
        /// <returns>执行函数的结果</returns>
        public static T SingleUserFunc<T>(string concurrentKey,T concurrentResult,Func<T> execFunc) 
        {
            double nowValue = redisHelper.StringIncrement(concurrentKey);
            redisHelper.KeyExpire(concurrentKey, TimeSpan.FromSeconds(300));//每次重置为5分钟过期
            if (nowValue > 1) return concurrentResult;//出现了并发
            try
            {
                return execFunc();
            }
            finally
            {
                redisHelper.StringSet(concurrentKey, "0", TimeSpan.FromSeconds(1));
            }
        }

        /// <summary>
        /// 控制总数量并发防超量来封装执行函数
        /// </summary>
        /// <typeparam name="T">执行函数的返回类型</typeparam>
        /// <param name="limitKey">控制总量并发的RedisKey</param>
        /// <param name="limitResult">发生总量并发超出时返回结果</param>
        /// <param name="nowValue">当前需要增加的数量</param>
        /// <param name="totalValue">需要控制的总数量</param>
        /// <param name="execfunc">单用户访问的执行函数</param>
        /// <returns>执行函数的结果</returns>
        public static T TotalLimitFunc<T>(string limitKey, T limitResult,double nowValue,double totalValue, Func<T> execfunc)
        {
            double userValue = redisHelper.StringIncrement(limitKey, nowValue);
            redisHelper.KeyExpire(limitKey, TimeSpan.FromSeconds(2592000));//每次重置30天有效时间
            if (userValue > totalValue)
            {
                try
                {
                    return limitResult;//当前已超出总量
                }
                finally
                {
                    redisHelper.StringDecrement(limitKey, nowValue);
                }
            }
            return execfunc();
        }
    }
}
