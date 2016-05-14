using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PhotoShare.DataLayer.Models.Mapping;

namespace PhotoShare.DataLayer.Models
{
    public partial class MtPhotosContext : DbContext
    {
        static MtPhotosContext()
        {
            Database.SetInitializer<MtPhotosContext>(null);
        }

        public MtPhotosContext()
            : base("Name=MtPhotosContext")
        {
        }

        public DbSet<MtDbFolder> MtDbFolders { get; set; }
        public DbSet<MtDbPhoto> MtDbPhotoes { get; set; }
        public DbSet<MtDbPhotoSale> MtDbPhotoSales { get; set; }
        public DbSet<MtIpnTest> MtIpnTests { get; set; }
        public DbSet<MtSetting> MtSettings { get; set; }
        public DbSet<vwPhotosAndSale> vwPhotosAndSales { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MtDbFolderMap());
            modelBuilder.Configurations.Add(new MtDbPhotoMap());
            modelBuilder.Configurations.Add(new MtDbPhotoSaleMap());
            modelBuilder.Configurations.Add(new MtIpnTestMap());
            modelBuilder.Configurations.Add(new MtSettingMap());
            modelBuilder.Configurations.Add(new vwPhotosAndSaleMap());
        }
    }
}
