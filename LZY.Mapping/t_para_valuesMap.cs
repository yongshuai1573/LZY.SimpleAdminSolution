using LZY.Model;
using Microsoft.EntityFrameworkCore;
namespace LZY.Mapping
{
    public class t_para_valuesMap : EntityTypeConfiguration<t_para_values>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_para_values>().ToTable("t_para_values").HasKey(_ => _.p_id);
            modelBuilder.Entity<t_para_values>().Property(a => a.p_id).UseSqlServerIdentityColumn();
        }
    }
}