namespace InterfaceProject.NetModel.CoreDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InterfaceCoreDB : DbContext
    {
        public InterfaceCoreDB()
            : base("name=InterfaceCoreDB")
        {
        }

        public virtual DbSet<JokeInfo> JokeInfo { get; set; }
        public virtual DbSet<SysConfigInfo> SysConfigInfo { get; set; }
        public virtual DbSet<WeatherCity> WeatherCity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
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
