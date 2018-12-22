using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Letter
    {
        public Letter()
        {
            Photos = new List<PhotoModel>();
        }

        public string ViewingCode { get; set; }
        public decimal PricePerPhoto { get; set; }
        public List<PhotoModel> Photos { get; set; } 
    }

    public class PhotoModel
    {
        public System.Guid Id { get; set; }
        public string DbName { get; set; }
    }
}