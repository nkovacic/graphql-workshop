using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Extensions;
//using ConferencePlanner.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Linq;

namespace ConferencePlanner.GraphQL.Speakers
{
    [ExtendObjectType(Name = "Query")]
    public class SpeakerQueries
    {
        [UseApplicationDbContext]
        [UseAutomapperProjection(typeof(SpeakerDto))]
        [UseFirstOrDefault]
        public IQueryable<Speaker> GetSpeaker([ScopedService] ApplicationDbContext context)
        {
            return context.Speakers;
        }


        [UseApplicationDbContext]
        //[UseAutomapperProjection(typeof(SpeakerDto))]
        [UseFirstOrDefault]
        public IQueryable<SpeakerDto> GetSpeakerNormal([ScopedService] ApplicationDbContext context, [Service]IMapper mapper)
        {
            return context.Speakers.ProjectTo<SpeakerDto>(mapper.ConfigurationProvider);
        }

        //[UseApplicationDbContext]
        //[UsePaging]
        //[UseProjection]
        //[UseSorting]
        //[UseFiltering]
        //public IQueryable<SessionDto> GetSessions(
        // [ScopedService] ApplicationDbContext context,
        //   [Service] IMapper mapper)
        //{

        //    var query = context.Sessions
        //           .ProjectTo<SessionDto>(mapper.ConfigurationProvider);

        //    return query;
        //}

        //[UseApplicationDbContext]
        //[UsePaging]
        //[UseProjection]
        //[UseSorting]
        //[UseFiltering]
        //public IQueryable<AttendeeDto> GetAttendees(
        //[ScopedService] ApplicationDbContext context,
        //  [Service] IMapper mapper)
        //{

        //    var query = context.Attendees
        //           .ProjectTo<AttendeeDto>(mapper.ConfigurationProvider);

        //    return query;
        //}


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