using System;
using System.Collections.Generic;
using System.Linq;
using Elo.Adworks;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Elo.GraphQL
{

    public class StoreDemographicsType : ObjectGraphType<StoreDemographics>
    {
        public StoreDemographicsType(AdventureworksDataContext dataStore)
        {
            Field(o => o.BusinessEntityId);
            Field(o => o.Name);
            Field(o => o.BankName);
            Field(o => o.AnnualSales);
            Field(o => o.AnnualRevenue);
            Field(o => o.BusinessType);
            Field(o => o.YearOpened);
            Field(o => o.Specialty);
            Field(o => o.SquareFeet);
            Field(o => o.Brands);
            Field(o => o.Internet);
            Field(o => o.NumberEmployees);
        }
    }

    public class StoreDemographicsQuery : GenericQuery<StoreDemographicsType, StoreDemographics>
    {
        public StoreDemographicsQuery(AdventureworksDataContext repo)
            : base(repo.StoreDemographics, GetTypeDescriptor())
        {

        }

        private static GraphTypeDescriptor<StoreDemographics> GetTypeDescriptor()
        {
            return new GraphTypeDescriptor<StoreDemographics>()
            {
                ObjectName = "demographic",
                ObjectPluralName = "demographics",
                IdParameterName = "id",
                IdParameterDescription = "id parameter",
                SearchParameterName = "filter",
                SearchParameterDescription = "search parameter",
                IdFieldValue = model => model.BusinessEntityId,
                Search = (model, filter) => model.Name.ContainsIgnorecase(filter)
            };
        }

    }
}