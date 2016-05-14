using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ProgressModel
    {
        public System.Guid Id { get; set; }
        public string Message { get; set; }
        public int Percentage { get; set; }
    }
}