using Microsoft.EntityFrameworkCore;

namespace InterfaceProject.Model.CoreDB
{
    /// <summary>
    /// ����������Ŀ�DB
    /// </summary>
    public partial class InterfaceCoreDB : DbContext
    {
        public InterfaceCoreDB(DbContextOptions<InterfaceCoreDB> interfaceCoreDB)
            : base(interfaceCoreDB)
        {
        }

        /// <summary>
        /// Ц����ϢDb����
        /// </summary>
        public virtual DbSet<JokeInfo> JokeInfo { get; set; }
        /// <summary>
        /// ϵͳ������ϢDb����
        /// </summary>
        public virtual DbSet<SysConfigInfo> SysConfigInfo { get; set; }
        /// <summary>
        /// ����Ԥ���ĳ�����Ϣ
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
