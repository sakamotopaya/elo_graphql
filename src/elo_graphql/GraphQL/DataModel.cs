using System.Collections.Generic;
using System.Linq;
using GraphQL;

namespace Elo.GraphQL
{
    public class QueryModel
    {
        public string Query { get; set; }
    }

    public class QueryX
    {
        private List<DataModel> _models = new List<DataModel>
                {
                    new DataModel { Id = "1", Hello = "Hello World10!", Yellow = "Yellow World10!" },
                    new DataModel { Id = "2", Hello = "Hello World20!", Yellow = "Yellow World20!" }
                };

        [GraphQLMetadata("dataModel")]
        public DataModel Get(string id)
        {
            return _models.FirstOrDefault(x => x.Id == id);
        }
        [GraphQLMetadata("dataModels")]
        public IEnumerable<DataModel> GetAll()
        {
            return _models;
        }
    }

    public class DataModel
    {
        public string Id { get; set; }
        public string Hello { get; set; }
        public string Yellow { get; set; }
    }
}