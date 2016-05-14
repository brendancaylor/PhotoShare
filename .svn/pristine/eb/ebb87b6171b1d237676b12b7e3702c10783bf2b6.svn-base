using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoShare.DataLayer.Models.Mapping
{
    public class MtSettingMap : EntityTypeConfiguration<MtSetting>
    {
        public MtSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DropboxAccessToken)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.DropboxRootFolder)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PayPalId)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("MtSettings");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DropboxAccessToken).HasColumnName("DropboxAccessToken");
            this.Property(t => t.DropboxRootFolder).HasColumnName("DropboxRootFolder");
            this.Property(t => t.PayPalId).HasColumnName("PayPalId");
        }
    }
}
