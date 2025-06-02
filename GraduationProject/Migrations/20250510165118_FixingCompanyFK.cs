using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class FixingCompanyFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_CompanyProfile_CompanyuserId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyuserId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyuserId",
                table: "Jobs");

            migrationBuilder.AlterColumn<string>(
                name: "companyProfileId",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_companyProfileId",
                table: "Jobs",
                column: "companyProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_CompanyProfile_companyProfileId",
                table: "Jobs",
                column: "companyProfileId",
                principalTable: "CompanyProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_CompanyProfile_companyProfileId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_companyProfileId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "companyProfileId",
                table: "Jobs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CompanyuserId",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyuserId",
                table: "Jobs",
                column: "CompanyuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_CompanyProfile_CompanyuserId",
                table: "Jobs",
                column: "CompanyuserId",
                principalTable: "CompanyProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
