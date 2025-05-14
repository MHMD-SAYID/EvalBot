using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class relateimagetouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_CompanyProfile_companyProfileId",
                table: "UserImage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_UserProfile_userProfileId",
                table: "UserImage");

            migrationBuilder.DropIndex(
                name: "IX_UserImage_companyProfileId",
                table: "UserImage");

            migrationBuilder.DropColumn(
                name: "companyProfileId",
                table: "UserImage");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "UserImage",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserImage_userProfileId",
                table: "UserImage",
                newName: "IX_UserImage_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage_Users_userId",
                table: "UserImage",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_Users_userId",
                table: "UserImage");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserImage",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserImage_userId",
                table: "UserImage",
                newName: "IX_UserImage_userProfileId");

            migrationBuilder.AddColumn<string>(
                name: "companyProfileId",
                table: "UserImage",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserImage_companyProfileId",
                table: "UserImage",
                column: "companyProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage_CompanyProfile_companyProfileId",
                table: "UserImage",
                column: "companyProfileId",
                principalTable: "CompanyProfile",
                principalColumn: "userId");

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
