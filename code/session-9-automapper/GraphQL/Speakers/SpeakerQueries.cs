using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
//using ConferencePlanner.GraphQL.DataLoader;
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
        [UseFiltering]
        public IQueryable<SpeakerDto> GetSpeakers(
          [ScopedService] ApplicationDbContext context,
            [Service] IMapper mapper)
        {

            var query = context.Speakers
                   .ProjectTo<SpeakerDto>(mapper.ConfigurationProvider);

            return query;
        }

        [UseApplicationDbContext]
        [UsePaging]
        [UseProjection]
        [UseSorting]

        [UseFiltering]
        public IQueryable<SpeakerDto> GetSpeakers2(
        [ScopedService] ApplicationDbContext context)
        {

            var query = context.Speakers
                .Select(x => new SpeakerDto()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Sessions = x.Sessions
                    .Select(y => new SessionDto()
                    {
                        Id = y.Id,
                        Title = y.Title
                    })
                });


            return query;
        }


        [UseApplicationDbContext]
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [UseFiltering]
        public IQueryable<Speaker> GetSpeakers3(
       [ScopedService] ApplicationDbContext context)
        {
            return context.Speakers;
        }


    }
}