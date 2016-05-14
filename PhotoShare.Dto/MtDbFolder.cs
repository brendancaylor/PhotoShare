using System;
using System.Collections.Generic;

namespace PhotoShare.Dto
{
    public partial class MtDbFolder
    {
        public MtDbFolder()
        {
            this.MtDbPhotoes = new List<MtDbPhoto>();
        }

        public System.Guid Id { get; set; }
        public int AppId { get; set; }
        public string DbName { get; set; }
        public string DbPath { get; set; }
        public bool IsIncluded { get; set; }
        public string ViewingCode { get; set; }
        public Nullable<decimal> PricePerPhoto { get; set; }
        public Nullable<int> TotalSold { get; set; }
        public Nullable<decimal> TotalSales { get; set; }
        public Nullable<int> SetsOf { get; set; }
        public virtual ICollection<MtDbPhoto> MtDbPhotoes { get; set; }
    }
}
