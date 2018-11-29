using LZY.Model;
using Microsoft.EntityFrameworkCore;
namespace LZY.Mapping
{
    public class t_userMap : EntityTypeConfiguration<t_user>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_user>().ToTable("t_user").HasKey(_ => _.p_id);
            modelBuilder.Entity<t_user>().Property(a => a.p_id).UseSqlServerIdentityColumn();
        }
    }
}