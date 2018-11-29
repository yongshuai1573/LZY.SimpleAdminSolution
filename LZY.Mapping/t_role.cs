using LZY.Model;
using Microsoft.EntityFrameworkCore;
namespace LZY.Mapping
{
    public class t_roleMap : EntityTypeConfiguration<t_role>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_role>().ToTable("t_role").HasKey(_ => _.p_id);
            modelBuilder.Entity<t_role>().Property(a => a.p_id).UseSqlServerIdentityColumn();
        }
    }
}