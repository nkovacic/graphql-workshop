using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencePlanner.GraphQL.Data
{
    public class SessionDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(4000)]
        public string? Abstract { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        // Bonus points to those who can figure out why this is written this way
        public TimeSpan Duration =>
            EndTime?.Subtract(StartTime ?? EndTime ?? DateTimeOffset.MinValue) ??
                TimeSpan.Zero;

        public int? TrackId { get; set; }

        public ICollection<SessionSpeakerDto> SessionSpeakers { get; set; } =
            new List<SessionSpeakerDto>();

        public ICollection<SessionAttendeeDto> SessionAttendees { get; set; } =
            new List<SessionAttendeeDto>();

        public Track? Track { get; set; }
    }
}