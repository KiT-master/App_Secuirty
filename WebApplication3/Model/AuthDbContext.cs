using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication3.Model
{



    public class AuthDbContext : IdentityDbContext<CustomUser>
    {
        private readonly IConfiguration _configuration;

        //public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        public AuthDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("AuthConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
     
    }


    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<CustomUser>
    {
        void IEntityTypeConfiguration<CustomUser>.Configure(EntityTypeBuilder<CustomUser> builder)
        {
            //builder.Property(u => u.FName).HasMaxLength(50);
            //builder.Property(u => u.LName).HasMaxLength(50);
            //builder.Property(u => u.Birthday).HasColumnType("datetime").IsRequired(false);
        }


    }



}
