using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class deleteuserimageFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_UserProfile_userProfileId",
                table: "UserImage");

            migrationBuilder.DropIndex(
                name: "IX_UserImage_userProfileId",
                table: "UserImage");

            migrationBuilder.DropColumn(
                name: "userProfileId",
                table: "UserImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userProfileId",
                table: "UserImage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserImage_userProfileId",
                table: "UserImage",
                column: "userProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage_UserProfile_userProfileId",
                table: "UserImage",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
