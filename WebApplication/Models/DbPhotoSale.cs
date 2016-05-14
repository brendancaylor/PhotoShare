using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DbPhotoSale
    {
        public int Id { get; set; }
        public int DbPhotoId { get; set; }
        public string SaleToken { get; set; }
        public decimal PricePaid { get; set; }
        public string BuyersEmail { get; set; }
        public DbPhoto DbPhoto { get; set; }
        public string Txnid { get; set; }
        
    }
}