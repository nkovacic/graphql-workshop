namespace ConferencePlanner.GraphQL.Data
{
    public class SessionAttendeeDto
    {
        public int SessionId { get; set; }

        public SessionDto? Session { get; set; }

        public int AttendeeId { get; set; }

        public AttendeeDto? Attendee { get; set; }
    }
}