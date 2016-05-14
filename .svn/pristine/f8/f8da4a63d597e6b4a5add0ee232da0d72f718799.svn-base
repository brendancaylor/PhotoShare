using System;
using System.Collections.Generic;

namespace PhotoShare.DataLayer.Models
{
    public partial class MtDbPhoto
    {
        public MtDbPhoto()
        {
            this.MtDbPhotoSales = new List<MtDbPhotoSale>();
        }

        public System.Guid Id { get; set; }
        public System.Guid MtDbFolderId { get; set; }
        public string DbName { get; set; }
        public string DbPath { get; set; }
        public string DbShareUrl { get; set; }
        public string ViewingCode { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalSales { get; set; }
        public byte[] LargeImage { get; set; }
        public byte[] SmallImage { get; set; }
        public string PayPalId { get; set; }
        public int Width { get; set; }
        public virtual MtDbFolder MtDbFolder { get; set; }
        public virtual ICollection<MtDbPhotoSale> MtDbPhotoSales { get; set; }
    }
}
