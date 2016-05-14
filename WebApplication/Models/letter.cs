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
            PhotoIds = new List<Guid>();
        }

        public string ViewingCode { get; set; }
        public decimal PricePerPhoto { get; set; }
        public List<Guid> PhotoIds { get; set; } 
    }
}