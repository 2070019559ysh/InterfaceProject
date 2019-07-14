using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.Model.Monitor
{
    /// <summary>
    /// 接口项目基于监控的数据上下文
    /// </summary>
    public class InterfaceMonitorDB : DbContext
    {
        /// <summary>
        /// 实例化基于监控的数据上下文
        /// </summary>
        /// <param name="options">实例化选项参数</param>
        public InterfaceMonitorDB(DbContextOptions<InterfaceMonitorDB> options)
            : base(options)
        {
        }

        /// <summary>
        /// 请求相关日志
        /// </summary>
        public virtual DbSet<RequestLog> RequestLog { get; set; }
        /// <summary>
        /// 系统本身日志
        /// </summary>
        public virtual DbSet<SystemLog> SystemLog { get; set; }

        /// <summary>
        /// 在创建数据库模型时执行，内部可指定映射表类型等
        /// </summary>
        /// <param name="modelBuilder">模拟构建器对象</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestLog>()
                .Property(e => e.RequestUrl)
                .IsUnicode(false);

            modelBuilder.Entity<RequestLog>()
                .Property(e => e.RequestMethod)
                .IsUnicode(false);

            modelBuilder.Entity<RequestLog>()
                .Property(e => e.ReferenceTable)
                .IsUnicode(false);

            modelBuilder.Entity<RequestLog>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<RequestLog>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.Module)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.Version)
                .IsUnicode(false);
        }
    }
}
