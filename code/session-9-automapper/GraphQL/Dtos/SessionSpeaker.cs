namespace ConferencePlanner.GraphQL.Data
{
    public class SessionSpeakerDto
    {
        public int SessionId { get; set; }

        public SessionDto? Session { get; set; }

        public int SpeakerId { get; set; }

        public SpeakerDto? Speaker { get; set; }
    }
}