using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class AddVisionResultModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Q_AVisionResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questionNumber = table.Column<int>(type: "int", nullable: false),
                    interviewId = table.Column<int>(type: "int", nullable: false),
                    AverageConfidenceScore = table.Column<double>(type: "float", nullable: false),
                    AverageTensionScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Q_AVisionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Q_AVisionResults_Interview_interviewId",
                        column: x => x.interviewId,
                        principalTable: "Interview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Q_AVisionResults_interviewId",
                table: "Q_AVisionResults",
                column: "interviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Q_AVisionResults");
        }
    }
}
