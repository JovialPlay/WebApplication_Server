using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace WebDevLab2.Model.Context
{
    public class MainContext : IdentityDbContext
    {
        protected readonly IConfiguration Configuration;
        public MainContext(IConfiguration configuration) 
        { 
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

        /*
        #region Constructor

        public MainContext(DbContextOptions<MainContext>
        options)
        : base(options)
        { }
        #endregion
        */

        public virtual DbSet<Developer> Developer {  get; set; }
        public virtual DbSet<DeveloperCommentary> DeveloperCommentary { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameCommentary> GameCommentary { get; set; }
        public virtual DbSet<Player> Player { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Developer>(entity =>
            {
                entity.HasIndex(e => e.CompanyName).IsUnique();
                entity.HasIndex(e => e.Login).IsUnique();
                entity.HasMany(g => g.CreatedGames).WithOne(d => d.Developer);
                entity.HasMany(dc => dc.DeveloperCommentaries).WithOne(d => d.Developer);
            });
            modelBuilder.Entity<DeveloperCommentary>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.HasOne(p => p.Owner).WithMany(dc => dc.DeveloperComments);
                entity.HasOne(p => p.Developer).WithMany(dc => dc.DeveloperCommentaries);
            });
            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                //entity.HasOne(d => d.Developer).WithMany(p => p.CreatedGames);
            });
            modelBuilder.Entity<GameCommentary>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.HasOne(g=>g.Game).WithMany(gc=>gc.Comments).IsRequired();
                entity.HasOne(p => p.Owner).WithMany(gc => gc.GameComments).IsRequired();

            });
            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Login).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.HasMany(gc => gc.GameComments).WithOne(p => p.Owner);
                entity.HasMany(dc => dc.DeveloperComments).WithOne(d => d.Owner);
            });
        }
    }
}
