using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencePlanner.GraphQL.Data
{
    public class SpeakerDto
    {
           public int Id { get; set; }

       
           public string? Name { get; set; }
         
           public string? Bio { get; set; }
         
           public string? WebSite { get; set; }

           public IEnumerable<SessionDto> Sessions { get; set; } =    new List<SessionDto>();
       }
   }