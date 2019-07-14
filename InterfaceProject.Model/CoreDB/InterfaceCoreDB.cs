using Microsoft.EntityFrameworkCore;

namespace InterfaceProject.Model.CoreDB
{
    /// <summary>
    /// 互联服务核心库DB
    /// </summary>
    public partial class InterfaceCoreDB : DbContext
    {
        public InterfaceCoreDB(DbContextOptions<InterfaceCoreDB> interfaceCoreDB)
            : base(interfaceCoreDB)
        {
        }

        /// <summary>
        /// 笑话信息Db集合
        /// </summary>
        public virtual DbSet<JokeInfo> JokeInfo { get; set; }
        /// <summary>
        /// 系统配置信息Db集合
        /// </summary>
        public virtual DbSet<SysConfigInfo> SysConfigInfo { get; set; }
        /// <summary>
        /// 天气预报的城市信息
        /// </summary>
        public virtual DbSet<WeatherCity> WeatherCity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JokeInfo>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JokeInfo>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<SysConfigInfo>()
                .Property(e => e.Key)
                .IsUnicode(false);

            modelBuilder.Entity<SysConfigInfo>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<WeatherCity>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<WeatherCity>()
                .Property(e => e.ParentId)
                .IsUnicode(false);
        }
    }
}
