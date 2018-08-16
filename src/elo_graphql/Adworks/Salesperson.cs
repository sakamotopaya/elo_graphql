
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elo.Adworks {

    public class Salesperson
    {
        public int BusinessEntityId { get; set; }

        public string FirstName { get; set; }
    
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    
        public string EmailAddress { get; set; }
        
        public int? TerritoryId { get; set; }
    }
}