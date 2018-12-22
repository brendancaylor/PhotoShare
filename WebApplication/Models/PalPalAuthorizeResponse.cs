using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class PalPalAuthorizeResponse
    {
        public string id { get; set; }
        public string intent { get; set; }
        public string state { get; set; }
        public string cart { get; set; }
        public string photoCodes { get; set; }
        public DateTime create_time { get; set; }
        public Payer payer { get; set; }
        public List<Transaction> transactions { get; set; }
    }

    public class ShippingAddress
    {
        public string recipient_name { get; set; }
        public string line1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
    }

    public class PayerInfo
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string payer_id { get; set; }
        public string country_code { get; set; }
        public ShippingAddress shipping_address { get; set; }
    }

    public class Payer
    {
        public string payment_method { get; set; }
        public string status { get; set; }
        public PayerInfo payer_info { get; set; }
    }

    public class Amount
    {
        public string total { get; set; }
        public string currency { get; set; }
        public Details details { get; set; }
    }

    public class ItemList
    {
    }

    public class Details
    {
        public string subtotal { get; set; }
    }

    public class Sale
    {
        public string id { get; set; }
        public string state { get; set; }
        public string payment_mode { get; set; }
        public string protection_eligibility { get; set; }
        public string parent_payment { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public Amount amount { get; set; }
    }

    public class RelatedResource
    {
        public Sale sale { get; set; }
    }

    public class Transaction
    {
        public string invoice_number { get; set; }
        public Amount amount { get; set; }
        public ItemList item_list { get; set; }
        public List<RelatedResource> related_resources { get; set; }
    }

    
}