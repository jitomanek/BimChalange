using Bim.Core.Entity.Models;

using Microsoft.EntityFrameworkCore;

namespace Bim.Core.Entity
{
    public class BimContext : DbContext
    {
        public BimContext() { }

        public BimContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<TaskEntity> TaskEntity { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>(entity => {

                entity.HasKey(e => e.Id);
                entity.Property(e=>e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
