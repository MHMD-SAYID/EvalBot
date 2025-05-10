using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class CompanyImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "companyProfileId",
                table: "UserImage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserImage_companyProfileId",
                table: "UserImage",
                column: "companyProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage_CompanyProfile_companyProfileId",
                table: "UserImage",
                column: "companyProfileId",
                principalTable: "CompanyProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_CompanyProfile_companyProfileId",
                table: "UserImage");

            migrationBuilder.DropIndex(
                name: "IX_UserImage_companyProfileId",
                table: "UserImage");

            migrationBuilder.DropColumn(
                name: "companyProfileId",
                table: "UserImage");
        }
    }
}
