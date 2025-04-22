using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Ponumber { get; set; }
        public DateTime? Podate { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToPin { get; set; }
        public string ShipToPhone { get; set; }
        public string ShipToGst { get; set; }
        public string InvoiceToAddress { get; set; }
        public string InvoiceToCity { get; set; }
        public string InvoiceToState { get; set; }
        public string InvoiceToPin { get; set; }
        public string InvoiceToPhone { get; set; }
        public string Pocomments { get; set; }
        public decimal? OrderSubtotal { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? Freight { get; set; }
        public decimal? OrderTotal { get; set; }
        public bool OrderConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
