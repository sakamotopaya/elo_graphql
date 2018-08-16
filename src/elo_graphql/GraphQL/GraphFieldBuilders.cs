using System.Collections.Generic;
using GraphQL.Builders;
using GraphQL.Types;

namespace Elo.GraphQL {
    public static class GraphFieldBuilders {
        public static FieldBuilder<object, ModelType> IdField<GraphType, ModelType, ParamType>(this ComplexGraphType<object> parent, string fieldName, string paramName, string paramDescription) where GraphType : IGraphType {
                return parent.Field<GraphType, ModelType>()
                            .Name(fieldName)
                            .Argument<ParamType>(paramName, paramDescription);
        }

        public static FieldBuilder<object, List<ModelType>> FilteredListField<GraphType, ModelType, ParamType>(this ComplexGraphType<object> parent, string fieldName, string paramName, string paramDescription) where GraphType : IGraphType {
                return parent.Field<ListGraphType<GraphType>, List<ModelType>>()
                            .Name(fieldName)
                            .Argument<ParamType>(paramName, paramDescription);
        }
    }
}