using LZY.Model;
using Microsoft.EntityFrameworkCore;
namespace LZY.Mapping
{
    public class t_user_roleMap : EntityTypeConfiguration<t_user_role>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_user_role>().ToTable("t_user_role").HasKey(_ => _.p_id);
            modelBuilder.Entity<t_user_role>().Property(a => a.p_id).UseSqlServerIdentityColumn();
        }
    }
}