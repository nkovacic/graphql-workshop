using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencePlanner.GraphQL.Data
{
    public class TrackDto
    {
        public int Id { get; set; }
         
        public string? Name { get; set; }

        public ICollection<SessionDto> Sessions { get; set; } =
            new List<SessionDto>();
    }
}