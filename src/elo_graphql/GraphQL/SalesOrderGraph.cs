using System;
using System.Collections.Generic;
using System.Linq;
using Elo.Adworks;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Elo.GraphQL
{

    public class SalesOrderType : ObjectGraphType<SalesOrder>
    {
        public SalesOrderType(AdventureworksDataContext dataStore)
        {
            Field(o => o.SalesOrderId);
            Field(o => o.PurchaseOrderNumber, nullable: true);
            Field(o => o.AccountNumber);
            Field<ListGraphType<SalesOrderDetailType>, List<SalesOrderDetail>>()
                .Name("details")
                .ResolveAsync(async context =>
                {
                    var salesOrder = context.Source;
                    return await dataStore.SalesOrderDetails.Where(m => m.SalesOrderId == salesOrder.SalesOrderId).ToListAsync();
                }
            );
        }
    }

    public class SalesOrderQuery : GenericQuery<SalesOrderType, SalesOrder>
    {
        public SalesOrderQuery(AdventureworksDataContext repo)
            : base(repo.SalesOrders, GetTypeDescriptor())
        {

        }

        private static GraphTypeDescriptor<SalesOrder> GetTypeDescriptor()

        {
            return new GraphTypeDescriptor<SalesOrder>()
            {
                ObjectName = "salesorder",
                ObjectPluralName = "salesorders",
                IdParameterName = "id",
                IdParameterDescription = "id parameter",
                SearchParameterName = "filter",
                SearchParameterDescription = "search parameter",
                IdFieldValue = model => model.SalesOrderId,
                Search = (model, filter) => StringUtilities.ContainsIgnorecase(model.PurchaseOrderNumber, filter)
            };
        }

    }

}