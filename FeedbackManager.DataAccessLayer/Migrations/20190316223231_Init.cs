using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackManager.DataAccessLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatorName = table.Column<string>(nullable: true),
                    SurveyName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionName = table.Column<string>(nullable: true),
                    ShortComment = table.Column<string>(nullable: true),
                    SurveyId = table.Column<int>(nullable: true),
                    Answers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Survey",
                columns: new[] { "Id", "CreatedAt", "CreatorName", "Description", "SurveyName" },
                values: new object[] { 1, new DateTime(2019, 3, 16, 22, 32, 31, 274, DateTimeKind.Utc), "Jose Moreno", "Some text 1", "Survey1" });

            migrationBuilder.InsertData(
                table: "Survey",
                columns: new[] { "Id", "CreatedAt", "CreatorName", "Description", "SurveyName" },
                values: new object[] { 2, new DateTime(2019, 3, 16, 22, 32, 31, 274, DateTimeKind.Utc), "John Bleach", "Some text 2", "Survey2" });

            migrationBuilder.InsertData(
                table: "Survey",
                columns: new[] { "Id", "CreatedAt", "CreatorName", "Description", "SurveyName" },
                values: new object[] { 3, new DateTime(2019, 3, 16, 22, 32, 31, 274, DateTimeKind.Utc), "Anna Rodriguez", "Some text 3", "Survey3" });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "Id", "Answers", "QuestionName", "ShortComment", "SurveyId" },
                values: new object[] { 1, "Answer 1;Answer 2;Answer 3", "Question title 1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 1 });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "Id", "Answers", "QuestionName", "ShortComment", "SurveyId" },
                values: new object[] { 2, "Answer 1;Answer 2", "Question title 2", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 1 });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "Id", "Answers", "QuestionName", "ShortComment", "SurveyId" },
                values: new object[] { 3, "Answer 1;Answer 2;Answer 3", "Question title 1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur...", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Question_SurveyId",
                table: "Question",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Survey");
        }
    }
}
