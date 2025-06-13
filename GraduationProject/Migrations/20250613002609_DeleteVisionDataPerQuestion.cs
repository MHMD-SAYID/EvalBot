using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class DeleteVisionDataPerQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageConfidenceScore",
                table: "Q_A");

            migrationBuilder.DropColumn(
                name: "AverageTensionScore",
                table: "Q_A");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
