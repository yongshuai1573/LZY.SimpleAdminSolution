using LZY.Model;
using Microsoft.EntityFrameworkCore;
namespace LZY.Mapping
{
    public class t_paratypeMap : EntityTypeConfiguration<t_paratype>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_paratype>().ToTable("t_paratype").HasKey(_ => _.p_id);
            modelBuilder.Entity<t_paratype>().Property(a => a.p_id).UseSqlServerIdentityColumn();
        }
    }
}