using System;
using System.Collections.Generic;
using System.Linq;
using Elo.Adworks;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Elo.GraphQL
{

    public class SalespersonType : ObjectGraphType<Salesperson>
    {
        public SalespersonType(AdventureworksDataContext dataStore)
        {
            Field(o => o.BusinessEntityId);
            Field(o => o.FirstName);
            Field(o => o.LastName);
            Field(o => o.EmailAddress);
            Field(o => o.PhoneNumber);
            Field<SalesTerritoryType, SalesTerritory>()
                .Name("territory")
                .ResolveAsync(async context =>
                {
                    var salesPerson = context.Source;
                    return await dataStore.SalesTerritories.FirstOrDefaultAsync(m => m.TerritoryId == salesPerson.TerritoryId);
                }
            );
        }
    }

public class SalespersonQuery : GenericQuery<SalespersonType, Salesperson>
    {
        public SalespersonQuery(AdventureworksDataContext repo)
            : base(repo.Salespersons, GetTypeDescriptor())
        {

        }
        
        private static GraphTypeDescriptor<Salesperson> GetTypeDescriptor()
        {
            return new GraphTypeDescriptor<Salesperson>()
            {
                ObjectName = "salesPerson",
                ObjectPluralName = "salesPersons",
                IdParameterName = "id",
                IdParameterDescription = "id parameter",
                SearchParameterName = "filter",
                SearchParameterDescription = "search parameter",
                IdFieldValue = model => model.BusinessEntityId,
                Search = (model,filter) => $"{model.FirstName} {model.LastName}".ContainsIgnorecase(filter)
            };
        }
    }
}