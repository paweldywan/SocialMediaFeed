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
    }
}
