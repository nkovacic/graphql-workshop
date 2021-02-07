using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencePlanner.GraphQL.Data
{
    public class Track
    {
        public int Id { get; set; }

        [StringLength(200)]
        [Required]
        public string? Name { get; set; }

        public ICollection<Session> Sessions { get; set; } =
            new List<Session>();
    }
}