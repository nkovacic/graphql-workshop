using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencePlanner.GraphQL.Data
{
    public class SessionDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Abstract { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        // Bonus points to those who can figure out why this is written this way
        public TimeSpan Duration =>
            EndTime?.Subtract(StartTime ?? EndTime ?? DateTimeOffset.MinValue) ??
                TimeSpan.Zero;

        public int? TrackId { get; set; }

        public ICollection<SpeakerDto> Speakers { get; set; } = new List<SpeakerDto>();

        //public ICollection<AttendeeDto> Attendees { get; set; } = new List<AttendeeDto>();

        public TrackDto? Track { get; set; }
    }
}