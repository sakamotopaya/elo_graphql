
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elo.Adworks;
using Elo.GraphQL;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Elo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly AdventureworksDataContext _context;
        private readonly IDocumentExecuter _documentExecutor;
        private readonly IObjectGraphSchemaProvider _schemaProvider;

        public QueryController(AdventureworksDataContext context, IDocumentExecuter documentExecuter,
                                IObjectGraphSchemaProvider schemaProvider)
        {
            _context = context;
            _documentExecutor = documentExecuter;
            _schemaProvider = schemaProvider;
        }

        [HttpPost("hello")]
        public ActionResult<string> HelloQuery([FromBody] QueryModel query)
        {
            var schema = Schema.For(@"
                                    type Query {
                                        hello: String
                                        yellow: String
                                    }
                                    ");

            var root = new { Id = "1", Hello = "Hello World!", Yellow = "Yellow World!" };

            var json = schema.Execute(_ =>
            {
                _.Query = query.Query;
                _.Root = root;
            });

            return json;
        }

        [HttpPost("{schemaName}")]
        public async Task<ActionResult<string>> StoreDemographics([FromRoute] string schemaName, [FromBody] QueryModel query)
        {

            try
            {

                var targetSchema = _schemaProvider.GetSchemaByName(schemaName);
                var executionOptions = new ExecutionOptions { Schema = targetSchema, Query = query.Query };
                var result = await _documentExecutor.ExecuteAsync(executionOptions).ConfigureAwait(false);

                if (result.Errors?.Count > 0)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (SchemaNotFoundException schemaException)
            {
                return BadRequest($"Schema not found {schemaException.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}