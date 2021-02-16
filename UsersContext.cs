using Microsoft.EntityFrameworkCore;

namespace Data
{
    public partial class UsersContext : DbContext
    {
        public DbSet<Base> Logins { get; set; }
        public DbSet<SMS> sMs { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Users;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Base>(entity =>
            {
                entity.ToTable("logins");
                
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
                
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SMS>(entity =>
            {
                entity.HasKey(e => e.Sender)
                   .HasName("PK__sms__333823AA8699F1B4");

                entity.ToTable("sms");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Sms)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SMS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
