using GraphQL.Types;

namespace Elo.GraphQL
{
    public interface IObjectGraphSchemaProvider
    {
        ISchema GetSchemaByName(string schemaName);
    }
}