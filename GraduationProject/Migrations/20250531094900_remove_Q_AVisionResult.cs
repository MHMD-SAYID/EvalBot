using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class remove_Q_AVisionResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Q_AVisionResults");

            migrationBuilder.AddColumn<double>(
                name: "AverageConfidenceScore",
                table: "Q_A",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AverageTensionScore",
                table: "Q_A",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "audioLink",
                table: "Q_A",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageConfidenceScore",
                table: "Q_A");

            migrationBuilder.DropColumn(
                name: "AverageTensionScore",
                table: "Q_A");

            migrationBuilder.DropColumn(
                name: "audioLink",
                table: "Q_A");

            migrationBuilder.CreateTable(
                name: "Q_AVisionResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interviewId = table.Column<int>(type: "int", nullable: false),
                    AverageConfidenceScore = table.Column<double>(type: "float", nullable: false),
                    AverageTensionScore = table.Column<double>(type: "float", nullable: false),
                    questionNumber = table.Column<int>(type: "int", nullable: false)
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
    }
}
