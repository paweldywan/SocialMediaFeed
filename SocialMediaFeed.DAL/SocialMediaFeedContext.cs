using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaFeed.DAL.Entities;

namespace SocialMediaFeed.DAL
{
    public class SocialMediaFeedContext(DbContextOptions options) : IdentityDbContext(options), IDataProtectionKeyContext
    {
        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialMediaFeedContext).Assembly);
        }
    }
}
