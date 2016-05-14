using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DbFolder
    {
        public DbFolder()
        {
            DbPhotos = new List<DbPhoto>();    
        }

        public Guid Id { get; set; }
        public int AppId { get; set; }
        public bool IsIncluded { get; set; }
        public string Actions { get; set; }
        public string DbName { get; set; }
        public string DbPath { get; set; }
        public string ViewToken { get; set; }

        [Display(Name = "Price per photo")]
        public decimal PricePerPhoto { get; set; }

        public int TotalSold { get; set; }
        public decimal TotalSales { get; set; }

        public List<DbPhoto> DbPhotos { get; set; }
    }
}