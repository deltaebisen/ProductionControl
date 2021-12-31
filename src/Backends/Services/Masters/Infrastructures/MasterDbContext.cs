using Masters.Models;
using Microsoft.EntityFrameworkCore;

namespace Masters.Infrastructures
{
    public class MasterDbContext : DbContext
    {
        public DbSet<DepartmentHistory> DepartmentHistories { get; set; }
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentHistory>()
                .HasKey(c => new { c.DepartmentCode, c.Revision });
        }
    }
}
