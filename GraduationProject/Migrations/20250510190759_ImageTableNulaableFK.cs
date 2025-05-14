using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class ImageTableNulaableFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_CompanyProfile_companyProfileId",
                table: "UserImage");

            migrationBuilder.AlterColumn<string>(
                name: "companyProfileId",
                table: "UserImage",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage_CompanyProfile_companyProfileId",
                table: "UserImage",
                column: "companyProfileId",
                principalTable: "CompanyProfile",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_CompanyProfile_companyProfileId",
                table: "UserImage");

            migrationBuilder.AlterColumn<string>(
                name: "companyProfileId",
                table: "UserImage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage_CompanyProfile_companyProfileId",
                table: "UserImage",
                column: "companyProfileId",
                principalTable: "CompanyProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
