using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate.Data;

namespace ConferencePlanner.GraphQL.Speakers
{
    [ExtendObjectType(Name = "Query")]
    public class SpeakerQueries
    {
       

        [UseApplicationDbContext]
        [UsePaging]
        [UseProjection]
        [UseSorting]
        public IQueryable<SpeakerDto> GetSpeakers(
          [ScopedService] ApplicationDbContext context,
            [Service] IMapper mapper)
        {

            var query = context.Speakers
                   .ProjectTo<SpeakerDto>(mapper.ConfigurationProvider);

            var expression = query.Expression;

            return query;
        }



        public Task<Speaker> GetSpeakerByIdAsync(
            [ID(nameof(Speaker))] int id,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            [ID(nameof(Speaker))] int[] ids,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            await dataLoader.LoadAsync(ids, cancellationToken);
    }
}