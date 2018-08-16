using System;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Elo.GraphQL
{

    public class ObjectGraphSchemaProvider : IObjectGraphSchemaProvider
    {
        private readonly System.IServiceProvider _sp;

        public ObjectGraphSchemaProvider(System.IServiceProvider sp)
        {
            _sp = sp;
        }

        public ISchema GetSchemaByName(string schemaName)
        {
            var query = default(IObjectGraphType);

            if (schemaName.EqualsIgnorecase("salesperson"))
                query = _sp.GetService<SalespersonQuery>();
            if (schemaName.EqualsIgnorecase("storedemographic"))
                query = _sp.GetService<StoreDemographicsQuery>();
            if (schemaName.EqualsIgnorecase("store"))
                query = _sp.GetService<StoreQuery>();
            if (schemaName.EqualsIgnorecase("territory"))
                query = _sp.GetService<SalesTerritoryQuery>();
            if (schemaName.EqualsIgnorecase("salesorder"))
                query = _sp.GetService<SalesOrderQuery>();
            if (schemaName.EqualsIgnorecase("salesorderdetail"))
                query = _sp.GetService<SalesOrderDetailQuery>();

            if (query != null)
            {
                var schema = new Schema(_sp.GetService<IDependencyResolver>());
                schema.Query = query;

                return schema;
            }

            throw new SchemaNotFoundException(schemaName);
        }
    }

    [Serializable]
    public class SchemaNotFoundException : Exception
    {
        public SchemaNotFoundException(string message) : base(message)
        {
        }

        public SchemaNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}