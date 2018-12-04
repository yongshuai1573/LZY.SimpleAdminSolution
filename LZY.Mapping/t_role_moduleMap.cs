using LZY.Model;
using Microsoft.EntityFrameworkCore;
namespace LZY.Mapping
{
    public class t_role_moduleMap : EntityTypeConfiguration<t_role_module>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_role_module>().ToTable("t_role_module").HasKey(_ => _.p_id);
            modelBuilder.Entity<t_role_module>().Property(a => a.p_id).UseSqlServerIdentityColumn();
        }
    }
}