 using AutoMapper;
using ConferencePlanner.GraphQL.Data; 

namespace GrowFlow.Domain.Dto.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Speaker, SpeakerDto>()
                //.ForMember(p => p.FlowerWeight, o => o.MapFrom(s => s.Weight)) 
                ;

            CreateMap<Attendee, AttendeeDto>()
                //.ForMember(p => p.IsForPlants, o => o.MapFrom(s => s.IsPlant))
                //.ForMember(p => p.IsForHarvests, o => o.MapFrom(s => s.Type == Enums.SiteType.Drying))
              ; 
                  
            CreateMap<Session, SessionDto>()
               ;
  
            CreateMap<Track, TrackDto>()
           ;
        }
    }
}
