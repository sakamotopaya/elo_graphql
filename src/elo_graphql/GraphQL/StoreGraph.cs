using System;
using System.Collections.Generic;
using System.Linq;
using Elo.Adworks;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Elo.GraphQL
{
    public class StoreType : ObjectGraphType<Store>
    {
        public StoreType(AdventureworksDataContext dataStore)
        {
            Field(o => o.BusinessEntityId);
            Field(o => o.Name);
            Field<SalespersonType, Salesperson>()
                .Name("salesperson")
                .ResolveAsync(async context =>
                {
                    var store = context.Source;
                    return await dataStore.Salespersons.FirstOrDefaultAsync(m => m.BusinessEntityId == store.SalespersonId);
                });

            Field<StoreDemographicsType, StoreDemographics>()
                .Name("demographic")
                .ResolveAsync(async context =>
                {
                    var store = context.Source;
                    return await dataStore.StoreDemographics.FirstOrDefaultAsync(m => m.BusinessEntityId == store.BusinessEntityId);
                }
            );
        }
    }

    public class StoreQuery : GenericQuery<StoreType, Store>
    {
        public StoreQuery(AdventureworksDataContext repo)
            : base(repo.Stores, GetTypeDescriptor())
        {

        }

        private static GraphTypeDescriptor<Store> GetTypeDescriptor()
        {
            return new GraphTypeDescriptor<Store>()
            {
                ObjectName = "store",
                ObjectPluralName = "stores",
                IdParameterName = "id",
                IdParameterDescription = "id parameter",
                SearchParameterName = "filter",
                SearchParameterDescription = "search parameter",
                IdFieldValue = model => model.BusinessEntityId,
                Search = (model,filter) => model.Name.ContainsIgnorecase(filter)
            };
        }
    }

}