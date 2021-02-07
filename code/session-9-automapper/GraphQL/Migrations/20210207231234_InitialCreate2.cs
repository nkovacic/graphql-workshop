using Microsoft.EntityFrameworkCore.Migrations;

namespace ConferencePlanner.GraphQL.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SessionSpeaker",
                columns: new[] { "SessionsId", "SpeakersId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SessionSpeaker",
                keyColumns: new[] { "SessionsId", "SpeakersId" },
                keyValues: new object[] { 1, 1 });
        }
    }
}
