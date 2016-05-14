using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class Folder
    {

        public Folder()
        {
            SetOptions = new Collection<SelectListItem>();
        }

        public Guid Id { get; set; }
        public bool IsIncluded { get; set; }
        public bool IsOnlyInDb { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        [Display(Name = "Price per photo")]
        public decimal PricePerPhoto { get; set; }

        [Display(Name = "Total sold")]
        public int TotalSold { get; set; }

        [Display(Name = "Total sales")]
        public decimal TotalSales { get; set; }

        [Display(Name = "Viewing code")]
        public string ViewingCode { get; set; }
        
        public int? SetsOf { get; set; }

        public virtual ICollection<System.Web.Mvc.SelectListItem> SetOptions { get; set; }
    }
}