using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Session> Sessions { get; set; } = default!;

        public DbSet<Track> Tracks { get; set; } = default!;

        public DbSet<Speaker> Speakers { get; set; } = default!;

        public DbSet<Attendee> Attendees { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Attendee>()
                .HasIndex(a => a.UserName)
                .IsUnique();



            // Many-to-many: Session <-> Attendee
            modelBuilder
                .Entity<SessionAttendee>()
                .HasKey(ca => new { ca.SessionId, ca.AttendeeId });

            // Many-to-many: Speaker <-> Session
            modelBuilder
                .Entity<SessionSpeaker>()
                .HasKey(ss => new { ss.SessionId, ss.SpeakerId });

            SeedData(modelBuilder);
        }


        protected void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Speaker>().HasData(SpeakerData);
             
            modelBuilder.Entity<Attendee>().HasData(AttendeesData);

            modelBuilder.Entity<Session>().HasData(SessionData);

            modelBuilder.Entity<SessionSpeaker>().HasData(SessionSpeakerData);
            
        }

        #region seed data
        private Attendee[] AttendeesData = new Attendee[] {
        new Attendee { Id = 1, FirstName = "Avengers: Endgame",LastName = "LastName",UserName = "UserName1", },
        new Attendee { Id = 2, FirstName = "The Lion King",LastName = "LastName",UserName = "UserName2", },
        new Attendee { Id = 3, FirstName = "Ip Man 4",LastName = "LastName",UserName = "UserName3", },
        new Attendee { Id = 4, FirstName = "Gemini Man",LastName = "LastName",UserName = "UserName4", },
        new Attendee { Id = 5, FirstName = "Downton Abbey",LastName = "LastName", UserName = "UserName5",},
         };
        private Speaker[] SpeakerData = new Speaker[] {
            new Speaker { Id = 1 , Name = "The Fresh Prince of Bel-Air", },
            new Speaker { Id = 2 , Name = "Downton Abbey", },
            new Speaker { Id = 3 , Name = "Stranger Things", },
            new Speaker { Id = 4 , Name = "Kantaro: The Sweet Tooth Salaryman", },
            new Speaker { Id = 5, Name = "The Walking Dead", },    };

        private Session[] SessionData = new Session[] {
        new Session { Id = 1 , Title = "The Fresh Prince of Bel-Air", },
            new Session { Id = 2 , Title = "Downton Abbey", },
            new Session { Id = 3 , Title = "Stranger Things", },
            new Session { Id = 4 , Title = "Kantaro: The Sweet Tooth Salaryman", },
            new Session { Id = 5, Title = "The Walking Dead", } };

        private SessionSpeaker[] SessionSpeakerData = new SessionSpeaker[] {
            new SessionSpeaker { SessionId = 1 , SpeakerId = 1 },
            new SessionSpeaker { SessionId = 2 , SpeakerId = 2 },
            new SessionSpeaker { SessionId = 3 , SpeakerId = 3 },
            new SessionSpeaker { SessionId = 4 , SpeakerId = 4 },
        };

        #endregion


    }
}