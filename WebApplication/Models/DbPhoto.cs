using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DbPhoto
    {
        public DbPhoto()
        {
            DbPhotoSales = new List<DbPhotoSale>();
        }

        public int Id { get; set; }
        public int DbFolderId { get; set; }
        public bool IsIncluded { get; set; }
        public string DbName { get; set; }
        public string DbPath { get; set; }
        public string DbShareUrl { get; set; }
        public string ViewToken { get; set; }

        public int TotalSold { get; set; }
        public decimal TotalSales { get; set; }

        public DbFolder DbFolder { get; set; }
        public List<DbPhotoSale> DbPhotoSales { get; set; }
    }
}