using Microsoft.EntityFrameworkCore;
using Connectedness.API.Models;

namespace Connectedness.API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

    }
}