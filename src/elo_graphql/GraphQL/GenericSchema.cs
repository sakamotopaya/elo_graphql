using System;
using System.Linq.Expressions;
using Elo.Adworks;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Elo.GraphQL
{

    public class GenericQuery<GraphType, ModelType> : ObjectGraphType
        where GraphType : IGraphType
        where ModelType : class
    {

        public GenericQuery(DbSet<ModelType> repo, GraphTypeDescriptor<ModelType> typeDescriptor)
        {
            Name = "Query";

            GraphFieldBuilders.IdField<GraphType,
                                    ModelType,
                                    NonNullGraphType<StringGraphType>>(this,
                                                                        typeDescriptor.ObjectName,
                                                                        typeDescriptor.IdParameterName,
                                                                        typeDescriptor.IdParameterDescription)
                            .ResolveAsync(async context =>
                            {
                                var id = context.GetArgument<int>("id");
                                var model = await repo.SingleOrDefaultAsync(m => id == typeDescriptor.IdFieldValue(m));
                                return model;
                            });

            GraphFieldBuilders.FilteredListField<GraphType,
                                                ModelType,
                                                StringGraphType>(this,
                                                                typeDescriptor.ObjectPluralName,
                                                                typeDescriptor.SearchParameterName,
                                                                typeDescriptor.SearchParameterDescription)
                            .Argument<IntGraphType>("pageNumber", "pageNumber to retrieve")
                            .Argument<IntGraphType>("recordsPerPage", "# of records per page")
                            .ResolveAsync(async context =>
                            {
                                var filter = context.GetArgument<string>("filter");
                                var pageNumber = context.GetArgument<int>("pageNumber");
                                var recordsPerPage = context.GetArgument<int>("recordsPerPage");

                                if (string.IsNullOrEmpty(filter))
                                    return await repo.PagedResults(pageNumber, recordsPerPage);
                                else
                                    return await repo.FilteredAndPagedResults(pageNumber, recordsPerPage, model => typeDescriptor.Search(model, filter) );

                            });

        }
    }
}