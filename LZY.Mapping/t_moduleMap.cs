using LZY.Model;
using Microsoft.EntityFrameworkCore;
namespace LZY.Mapping
{
    public class t_moduleMap : EntityTypeConfiguration<t_module>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_module>().ToTable("t_module").HasKey(_ => _.p_id);
            modelBuilder.Entity<t_module>().Property(a => a.p_id).UseSqlServerIdentityColumn();
        }
    }
}
