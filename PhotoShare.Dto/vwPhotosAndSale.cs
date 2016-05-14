using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Dto
{
    public class vwPhotosAndSale
    {
        public Nullable<System.Guid> Id { get; set; }
        public System.Guid MtDbPhotoId { get; set; }
        public string DbName { get; set; }
        public string ViewingCode { get; set; }
        public byte[] LargeImage { get; set; }
        public byte[] SmallImage { get; set; }
        public string PayPalId { get; set; }
        public int Width { get; set; }
        public System.Guid MtDbFolderId { get; set; }
        public Nullable<decimal> PricePerPhoto { get; set; }
        public string DbPath { get; set; }
        public string DbShareUrl { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalSales { get; set; }
        public string SaleCode { get; set; }
    }
}
