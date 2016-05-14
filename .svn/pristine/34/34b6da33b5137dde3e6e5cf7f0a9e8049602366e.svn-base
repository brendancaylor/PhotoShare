using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoShare.DataLayer.Models.Mapping
{
    public class MtDbFolderMap : EntityTypeConfiguration<MtDbFolder>
    {
        public MtDbFolderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DbName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.DbPath)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ViewingCode)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("MtDbFolder");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AppId).HasColumnName("AppId");
            this.Property(t => t.DbName).HasColumnName("DbName");
            this.Property(t => t.DbPath).HasColumnName("DbPath");
            this.Property(t => t.IsIncluded).HasColumnName("IsIncluded");
            this.Property(t => t.ViewingCode).HasColumnName("ViewingCode");
            this.Property(t => t.PricePerPhoto).HasColumnName("PricePerPhoto");
            this.Property(t => t.TotalSold).HasColumnName("TotalSold");
            this.Property(t => t.TotalSales).HasColumnName("TotalSales");
            this.Property(t => t.SetsOf).HasColumnName("SetsOf");
        }
    }
}
