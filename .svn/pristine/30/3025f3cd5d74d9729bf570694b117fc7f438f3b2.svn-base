using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoShare.DataLayer.Models.Mapping
{
    public class MtIpnTestMap : EntityTypeConfiguration<MtIpnTest>
    {
        public MtIpnTestMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.IpnMessage)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("MtIpnTest");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IpnMessage).HasColumnName("IpnMessage");
        }
    }
}
