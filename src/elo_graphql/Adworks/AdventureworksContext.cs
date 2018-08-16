
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Elo.Adworks
{
    public class AdventureworksDataContext : DbContext
    {

        public AdventureworksDataContext(DbContextOptions<AdventureworksDataContext> options) : base(options) { }

        public DbSet<SalesTerritory> SalesTerritories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<Salesperson> Salespersons { get; set; }
        public DbSet<StoreDemographics> StoreDemographics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sales");

            var salesPersonBuilder = modelBuilder.Entity<Salesperson>();
            salesPersonBuilder.ToTable("vsalesperson");
            salesPersonBuilder.HasKey(m => m.BusinessEntityId);
            salesPersonBuilder.Property(b => b.BusinessEntityId).HasColumnName("businessentityid").IsRequired();
            salesPersonBuilder.Property(m => m.FirstName).HasColumnName("firstname");
            salesPersonBuilder.Property(m => m.LastName).HasColumnName("lastname");
            salesPersonBuilder.Property(m => m.TerritoryId).HasColumnName("territoryid");
            salesPersonBuilder.Property(m => m.EmailAddress).HasColumnName("emailaddress");
            salesPersonBuilder.Property(m => m.PhoneNumber).HasColumnName("phonenumber");

        }
    }

}