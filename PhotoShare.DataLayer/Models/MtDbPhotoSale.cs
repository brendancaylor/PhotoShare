using System;
using System.Collections.Generic;

namespace PhotoShare.DataLayer.Models
{
    public partial class MtDbPhotoSale
    {
        public System.Guid Id { get; set; }
        public System.Guid MtDbPhotoId { get; set; }
        public string SaleCode { get; set; }
        public decimal PricePaid { get; set; }
        public System.DateTime DatePaid { get; set; }
        public string IpnMessage { get; set; }
        public string BuyersEmail { get; set; }
        public string Txnid { get; set; }
        public virtual MtDbPhoto MtDbPhoto { get; set; }
    }
}
