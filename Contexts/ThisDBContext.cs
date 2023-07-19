using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebApplication1.Models;

namespace WebApplication1.Contexts
{
    public class ThisDBContext : DbContext{
        public DbSet<SwiftMessage> ThisModels { get; set; }
        public DbSet<SwiftMessageContent> ContentModel { get; set; }
        public DbSet<SwiftMessageChecksum> ChecksumModel { get; set; }

        public ThisDBContext(DbContextOptions<ThisDBContext> options) : base(options){
        }

        public ThisDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<SwiftMessage>()
                .HasOne(s => s.Field4)
                .WithOne(c => c.SwiftMessage)
                .HasForeignKey<SwiftMessageContent>(c => c.SwiftMessageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SwiftMessage>()
                .HasOne(s => s.Field5)
                .WithOne(c => c.SwiftMessage)
                .HasForeignKey<SwiftMessageChecksum>(c => c.SwiftMessageId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
