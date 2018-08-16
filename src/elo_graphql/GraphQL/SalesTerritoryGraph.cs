using System;
using System.Collections.Generic;
using System.Linq;
using Elo.Adworks;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Elo.GraphQL
{

    public class SalesTerritoryType : ObjectGraphType<SalesTerritory>
    {
        public SalesTerritoryType(AdventureworksDataContext dataStore)
        {
            Field(o => o.TerritoryId);
            Field(o => o.Name);
            Field(o => o.Region);
            Field(o => o.SalesYtd);
        }
    }

    public class SalesTerritoryQuery : GenericQuery<SalesTerritoryType, SalesTerritory>
    {
                public SalesTerritoryQuery(AdventureworksDataContext repo)
            : base(repo.SalesTerritories, GetTypeDescriptor())
        {

        }

        private static GraphTypeDescriptor<SalesTerritory> GetTypeDescriptor()
        {    
            return new GraphTypeDescriptor<SalesTerritory>()
            {
                ObjectName = "territory",
                ObjectPluralName = "territories",
                IdParameterName = "id",
                IdParameterDescription = "id parameter",
                SearchParameterName = "filter",
                SearchParameterDescription = "search parameter",
                IdFieldValue = model => model.TerritoryId,
                Search = (model,filter) => model.Name.ContainsIgnorecase(filter)
            };
        }
    }
}