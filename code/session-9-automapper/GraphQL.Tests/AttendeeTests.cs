using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;
using HotChocolate;
using HotChocolate.Execution;
using Snapshooter.Xunit;
using Xunit;

namespace GraphQL.Tests
{
    public class AttendeeTests
    {
        

        [Fact]
        public async Task GetSpeakers()
        {
            // arrange
            IRequestExecutor executor = await new ServiceCollection()
                .AddPooledDbContextFactory<ApplicationDbContext>(
                     options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ContosoUniversity1;Trusted_Connection=True;MultipleActiveResultSets=true")
                      )
                .AddGraphQL()
               .AddQueryType(d => d.Name("Query")) 
                    .AddTypeExtension<SpeakerQueries>()
               //.AddType<AttendeeType>()
               //.AddType<SessionType>()
               //.AddType<SpeakerType>()
               //.AddType<TrackType>()
               .EnableRelaySupport()
               .BuildRequestExecutorAsync();

            // act
            IExecutionResult result = await executor.ExecuteAsync(@"
                query {
                  speakers{
                    nodes{
                      id
                      sessionSpeakers{
                        session{
                          id
                        }
                      }
                    }
                  }
                }");

            // assert
            result.ToJson().MatchSnapshot();
        }
    }
}