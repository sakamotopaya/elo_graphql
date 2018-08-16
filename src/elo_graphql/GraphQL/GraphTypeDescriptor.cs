using System;

namespace Elo.GraphQL
{
    public class GraphTypeDescriptor<ModelType> where ModelType : class
    {
        public string ObjectName { get; set; }
        public string ObjectPluralName { get; set; }
        public string IdParameterName { get; set; }
        public string IdParameterDescription { get; set; }
        public string SearchParameterName { get; set; }
        public string SearchParameterDescription { get; set; }

        public Func<ModelType, int> IdFieldValue { get; set; }
        public Func<ModelType, string, bool> Search { get; set; }
    }
}