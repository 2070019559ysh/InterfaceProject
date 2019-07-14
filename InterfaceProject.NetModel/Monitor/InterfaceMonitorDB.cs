using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace InterfaceProject.NetModel.Monitor
{

    public partial class InterfaceMonitorDB : DbContext
    {
        public InterfaceMonitorDB()
            : base("name=InterfaceMonitorDB")
        {
        }

        public virtual DbSet<RequestLog> RequestLog { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
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
