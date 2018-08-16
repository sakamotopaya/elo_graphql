using System;
using System.Collections.Generic;
using System.Linq;
using Elo.Adworks;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Elo.GraphQL
{

    public class SalesOrderDetailType : ObjectGraphType<SalesOrderDetail>
    {
        public SalesOrderDetailType(AdventureworksDataContext dataStore)
        {
            Field(o => o.SalesOrderDetailId);
            Field(o => o.SalesOrderId);
            Field(o => o.CarrierTrackingNumber);
            Field(o=> o.ProductId);
            Field(o => o.Quantity);
            Field(o => o.UnitPrice);
            Field<DecimalGraphType, decimal>("lineTotal").Resolve(context => context.Source.Quantity * context.Source.UnitPrice);
        }
    }

public class SalesOrderDetailQuery : GenericQuery<SalesOrderDetailType, SalesOrderDetail>
    {

        public SalesOrderDetailQuery(AdventureworksDataContext repo)
            : base(repo.SalesOrderDetails, GetTypeDescriptor())
        {

        }

        private static GraphTypeDescriptor<SalesOrderDetail> GetTypeDescriptor()
        {
            return new GraphTypeDescriptor<SalesOrderDetail>()
            {
                ObjectName = "salesorderdetail",
                ObjectPluralName = "salesorderdetails",
                IdParameterName = "id",
                IdParameterDescription = "id parameter",
                SearchParameterName = "salesorderid",
                SearchParameterDescription = "search parameter",
                IdFieldValue = model => model.SalesOrderDetailId,
                Search = (model,filter) => Convert.ToString(model.SalesOrderId) == filter 
            };
        }
    }

}