using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoShare.DataLayer.Models.Mapping
{
    public class MtDbPhotoSaleMap : EntityTypeConfiguration<MtDbPhotoSale>
    {
        public MtDbPhotoSaleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SaleCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.IpnMessage)
                .IsRequired();

            this.Property(t => t.BuyersEmail)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Txnid)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("MtDbPhotoSale");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MtDbPhotoId).HasColumnName("MtDbPhotoId");
            this.Property(t => t.SaleCode).HasColumnName("SaleCode");
            this.Property(t => t.PricePaid).HasColumnName("PricePaid");
            this.Property(t => t.DatePaid).HasColumnName("DatePaid");
            this.Property(t => t.IpnMessage).HasColumnName("IpnMessage");
            this.Property(t => t.BuyersEmail).HasColumnName("BuyersEmail");
            this.Property(t => t.Txnid).HasColumnName("Txnid");

            // Relationships
            this.HasRequired(t => t.MtDbPhoto)
                .WithMany(t => t.MtDbPhotoSales)
                .HasForeignKey(d => d.MtDbPhotoId);

        }
    }
}
