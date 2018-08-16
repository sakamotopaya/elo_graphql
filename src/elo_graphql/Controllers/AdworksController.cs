using System.Collections.Generic;
using System.Threading.Tasks;
using Elo.Adworks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Elo
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdworksController
    {
        private readonly IConfiguration _config;
        private readonly AdventureworksDataContext _context;

        public AdworksController(IConfiguration config, AdventureworksDataContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpGet("test")]
        public async Task<ActionResult<List<Salesperson>>> Test()
        {
            var models = new List<Salesperson>();
            var connectionString = _config[SystemConstants.ConnectionStringKey];

            using (var connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new Npgsql.NpgsqlCommand(@"SELECT v.businessentityid, v.emailaddress, v.firstname, v.lastname, v.phonenumber, v.territoryid FROM sales.vsalesperson AS v", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var model = new Salesperson();
                        model.BusinessEntityId = await reader.GetFieldValueAsync<int>(reader.GetOrdinal("businessentityid"));
                        model.FirstName = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("firstname"));
                        model.TerritoryId = await reader.GetFieldValueAsync<int?>(reader.GetOrdinal("territoryid"));

                        models.Add(model);
                    }

                }
            }
            return models;
        }

        [HttpGet("salesperson")]
        public async Task<ActionResult<List<Salesperson>>> AllSalespersons([FromQuery]int pageNumber = 1,
                                                                            [FromQuery]int pageSize = 10)
        {
            return await _context.Salespersons.PagedResults(pageNumber, pageSize);
        }

        [HttpGet("store")]
        public async Task<ActionResult<List<Store>>> AllStores([FromQuery]int pageNumber = 1,
                                                                [FromQuery]int pageSize = 10)
        {
            return await _context.Stores.PagedResults(pageNumber, pageSize);
        }

        [HttpGet("storedemographic")]
        public async Task<ActionResult<List<StoreDemographics>>> AllStoreDemographics([FromQuery]int pageNumber = 1,
                                                                                    [FromQuery]int pageSize = 10)
        {
            return await _context.StoreDemographics.PagedResults(pageNumber, pageSize);
        }

        [HttpGet("territory")]
        public async Task<ActionResult<List<SalesTerritory>>> AllTerritories([FromQuery]int pageNumber = 1,
                                                                            [FromQuery]int pageSize = 10)
        {
            return await _context.SalesTerritories.PagedResults(pageNumber, pageSize);
        }

        [HttpGet("salesorder")]
        public async Task<ActionResult<List<SalesOrder>>> AllSalesOrders([FromQuery]int pageNumber = 1,
                                                                            [FromQuery]int pageSize = 10)
        {
            return await _context.SalesOrders.PagedResults(pageNumber, pageSize);
        }

        [HttpGet("salesorderdetail")]
        public async Task<ActionResult<List<SalesOrderDetail>>> AllSalesOrderDetails([FromQuery]int pageNumber = 1,
                                                                                    [FromQuery]int pageSize = 10)
        {
            return await _context.SalesOrderDetails.PagedResults(pageNumber, pageSize);
        }
    }
}