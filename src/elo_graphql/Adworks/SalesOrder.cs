using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elo.Adworks
{
    [Table("salesorderheader")]
    public class SalesOrder
    {

        [Column("salesorderid"), Key]
        public int SalesOrderId { get; set; }

        [Column("purchaseordernumber")]
        public string PurchaseOrderNumber { get; set; }

        [Column("accountnumber")]
        public string AccountNumber { get; set; }

        [Column("customerid")]
        public int CustomerId { get; set; }

        [Column("salespersonid")]
        public int? SalespersonId { get; set; }
    }
}