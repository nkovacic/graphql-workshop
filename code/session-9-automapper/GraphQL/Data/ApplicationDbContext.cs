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

             
        }

        #region seed data
        private Attendee[] AttendeesData = new Attendee[] {
        new Attendee { Id = 1, FirstName = "Avengers: Endgame",LastName = "LastName",UserName = "UserName", },
        new Attendee { Id = 2, FirstName = "The Lion King",LastName = "LastName",UserName = "UserName", },
        new Attendee { Id = 3, FirstName = "Ip Man 4",LastName = "LastName",UserName = "UserName", },
        new Attendee { Id = 4, FirstName = "Gemini Man",LastName = "LastName",UserName = "UserName", },
        new Attendee { Id = 5, FirstName = "Downton Abbey",LastName = "LastName", UserName = "UserName",},
    };
        private Speaker[] SpeakerData = new Speaker[] {
        new Speaker { Id = 6 , Name = "The Fresh Prince of Bel-Air", },
            new Speaker { Id = 7 , Name = "Downton Abbey", },
            new Speaker { Id = 8 , Name = "Stranger Things", },
            new Speaker { Id = 9 , Name = "Kantaro: The Sweet Tooth Salaryman", },
            new Speaker { Id = 10, Name = "The Walking Dead", },    };

        private Session[] SessionData = new Session[] {
        new Session { Id = 6 , Title = "The Fresh Prince of Bel-Air", },
            new Session { Id = 7 , Title = "Downton Abbey", },
            new Session { Id = 8 , Title = "Stranger Things", },
            new Session { Id = 9 , Title = "Kantaro: The Sweet Tooth Salaryman", },
            new Session { Id = 10, Title = "The Walking Dead", } };
        #endregion


    }
}