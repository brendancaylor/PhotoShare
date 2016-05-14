using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoShare.DataLayer.Models.Mapping
{
    public class MtDbPhotoMap : EntityTypeConfiguration<MtDbPhoto>
    {
        public MtDbPhotoMap()
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

            this.Property(t => t.DbShareUrl)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ViewingCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.LargeImage)
                .IsRequired();

            this.Property(t => t.SmallImage)
                .IsRequired();

            this.Property(t => t.PayPalId)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("MtDbPhoto");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MtDbFolderId).HasColumnName("MtDbFolderId");
            this.Property(t => t.DbName).HasColumnName("DbName");
            this.Property(t => t.DbPath).HasColumnName("DbPath");
            this.Property(t => t.DbShareUrl).HasColumnName("DbShareUrl");
            this.Property(t => t.ViewingCode).HasColumnName("ViewingCode");
            this.Property(t => t.TotalSold).HasColumnName("TotalSold");
            this.Property(t => t.TotalSales).HasColumnName("TotalSales");
            this.Property(t => t.LargeImage).HasColumnName("LargeImage");
            this.Property(t => t.SmallImage).HasColumnName("SmallImage");
            this.Property(t => t.PayPalId).HasColumnName("PayPalId");
            this.Property(t => t.Width).HasColumnName("Width");

            // Relationships
            this.HasRequired(t => t.MtDbFolder)
                .WithMany(t => t.MtDbPhotoes)
                .HasForeignKey(d => d.MtDbFolderId);

        }
    }
}
