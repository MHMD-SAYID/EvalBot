using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTrackFromQ_A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Q_A_Track_TrackId",
                table: "Q_A");

            migrationBuilder.DropIndex(
                name: "IX_Q_A_TrackId",
                table: "Q_A");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "Q_A");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "Q_A",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Q_A_TrackId",
                table: "Q_A",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Q_A_Track_TrackId",
                table: "Q_A",
                column: "TrackId",
                principalTable: "Track",
                principalColumn: "Id");
        }
    }
}
