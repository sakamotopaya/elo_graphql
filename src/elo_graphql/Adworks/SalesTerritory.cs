using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elo.Adworks {
    
    [Table("salesterritory")]
    public class SalesTerritory
    {

        [Column("territoryid"), Key]
        public int TerritoryId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("countryregioncode")]
        public string Region { get; set; }
        [Column("group")]
        public string Group { get; set; }
        [Column("salesytd")]
        public decimal SalesYtd { get; set; }
    }
}