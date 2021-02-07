using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ConferencePlanner.GraphQL;
using ConferencePlanner.GraphQL.Attendees;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Sessions;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Tracks;
using ConferencePlanner.GraphQL.Types;
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
               .AddType<AttendeeType>()
               .AddType<SessionType>()
               .AddType<SpeakerType>()
               .AddType<TrackType>()
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