using LZY.Model;
using Microsoft.EntityFrameworkCore;
namespace LZY.Mapping
{
    public class t_syslogMap : EntityTypeConfiguration<t_syslog>
    {
        public override void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_syslog>().ToTable("t_syslog").HasKey(_ => _.p_id);
            modelBuilder.Entity<t_syslog>().Property(a => a.p_id).UseSqlServerIdentityColumn();
        }
    }
}