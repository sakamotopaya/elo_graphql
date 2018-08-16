using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Elo.Adworks
{
    [Table("salesorderdetail")]
    public class SalesOrderDetail
    {

        [Column("salesorderid")]
        public int SalesOrderId { get; set; }

        [Column("salesorderdetailid"), Key]
        public int SalesOrderDetailId { get; set; }

        [Column("carriertrackingnumber")]
        public string CarrierTrackingNumber { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("orderqty")]
        public int Quantity { get; set; }

        [Column("unitprice")]
        public decimal UnitPrice { get; set; }
    }
}