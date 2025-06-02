using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class companyHandling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_businessAccounts_AspNetUsers_UserId",
                table: "businessAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_AspNetUsers_UserId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_AspNetUsers_UserId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Interview_AspNetUsers_UserId",
                table: "Interview");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Company_CompanyId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_AspNetUsers_userId",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCV_AspNetUsers_userId",
                table: "UserCV");

            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_AspNetUsers_userId",
                table: "UserImage");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "JobUser");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CountryOfResidence",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpectedSalary",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IDCard",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "role",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserImage",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserImage_userId",
                table: "UserImage",
                newName: "IX_UserImage_userProfileId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserCV",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCV_userId",
                table: "UserCV",
                newName: "IX_UserCV_userProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Projects",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                newName: "IX_Projects_userProfileId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Language",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Language_userId",
                table: "Language",
                newName: "IX_Language_userProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Interview",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Interview_UserId",
                table: "Interview",
                newName: "IX_Interview_userProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Experience",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Experience_UserId",
                table: "Experience",
                newName: "IX_Experience_userProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Education",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_UserId",
                table: "Education",
                newName: "IX_Education_userProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "businessAccounts",
                newName: "userProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_businessAccounts_UserId",
                table: "businessAccounts",
                newName: "IX_businessAccounts_userProfileId");

            migrationBuilder.AddColumn<string>(
                name: "CompanyuserId",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "companyProfileId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CompanyProfile",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProfile", x => x.userId);
                    table.ForeignKey(
                        name: "FK_CompanyProfile_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryOfResidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedSalary = table.Column<double>(type: "float", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.userId);
                    table.ForeignKey(
                        name: "FK_UserProfile_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobUserProfile",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    userProfileuserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobUserProfile", x => new { x.JobsId, x.userProfileuserId });
                    table.ForeignKey(
                        name: "FK_JobUserProfile_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobUserProfile_UserProfile_userProfileuserId",
                        column: x => x.userProfileuserId,
                        principalTable: "UserProfile",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyuserId",
                table: "Jobs",
                column: "CompanyuserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobUserProfile_userProfileuserId",
                table: "JobUserProfile",
                column: "userProfileuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_businessAccounts_UserProfile_userProfileId",
                table: "businessAccounts",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_UserProfile_userProfileId",
                table: "Education",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_UserProfile_userProfileId",
                table: "Experience",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_UserProfile_userProfileId",
                table: "Interview",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_CompanyProfile_CompanyuserId",
                table: "Jobs",
                column: "CompanyuserId",
                principalTable: "CompanyProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Language_UserProfile_userProfileId",
                table: "Language",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserProfile_userProfileId",
                table: "Projects",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCV_UserProfile_userProfileId",
                table: "UserCV",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage_UserProfile_userProfileId",
                table: "UserImage",
                column: "userProfileId",
                principalTable: "UserProfile",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_businessAccounts_UserProfile_userProfileId",
                table: "businessAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_UserProfile_userProfileId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_UserProfile_userProfileId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Interview_UserProfile_userProfileId",
                table: "Interview");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_CompanyProfile_CompanyuserId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_UserProfile_userProfileId",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserProfile_userProfileId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCV_UserProfile_userProfileId",
                table: "UserCV");

            migrationBuilder.DropForeignKey(
                name: "FK_UserImage_UserProfile_userProfileId",
                table: "UserImage");

            migrationBuilder.DropTable(
                name: "CompanyProfile");

            migrationBuilder.DropTable(
                name: "JobUserProfile");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyuserId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyuserId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "companyProfileId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "UserImage",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserImage_userProfileId",
                table: "UserImage",
                newName: "IX_UserImage_userId");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "UserCV",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCV_userProfileId",
                table: "UserCV",
                newName: "IX_UserCV_userId");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "Projects",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_userProfileId",
                table: "Projects",
                newName: "IX_Projects_UserId");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "Language",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Language_userProfileId",
                table: "Language",
                newName: "IX_Language_userId");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "Interview",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Interview_userProfileId",
                table: "Interview",
                newName: "IX_Interview_UserId");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "Experience",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Experience_userProfileId",
                table: "Experience",
                newName: "IX_Experience_UserId");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "Education",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_userProfileId",
                table: "Education",
                newName: "IX_Education_UserId");

            migrationBuilder.RenameColumn(
                name: "userProfileId",
                table: "businessAccounts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_businessAccounts_userProfileId",
                table: "businessAccounts",
                newName: "IX_businessAccounts_UserId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryOfResidence",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "EmailType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ExpectedSalary",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "IDCard",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobUser",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobUser", x => new { x.JobsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_JobUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobUser_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobUser_UsersId",
                table: "JobUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_businessAccounts_AspNetUsers_UserId",
                table: "businessAccounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_AspNetUsers_UserId",
                table: "Education",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_AspNetUsers_UserId",
                table: "Experience",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_AspNetUsers_UserId",
                table: "Interview",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Company_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_AspNetUsers_userId",
                table: "Language",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCV_AspNetUsers_userId",
                table: "UserCV",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage_AspNetUsers_userId",
                table: "UserImage",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
