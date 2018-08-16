using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Elo.Adworks {
    
    [Table("store")]
    public class Store
    {

        [Column("businessentityid"), Key]
        public int BusinessEntityId { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("salespersonid")]
        public int SalespersonId { get; set; }
    }
}