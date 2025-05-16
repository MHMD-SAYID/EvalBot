using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class mayntomanyuserprofilejo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobUserProfile");

            migrationBuilder.CreateTable(
                name: "JobUserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    jobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobUserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobUserProfiles_Jobs_jobId",
                        column: x => x.jobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobUserProfiles_UserProfile_userProfileId",
                        column: x => x.userProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobUserProfiles_jobId",
                table: "JobUserProfiles",
                column: "jobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobUserProfiles_userProfileId",
                table: "JobUserProfiles",
                column: "userProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobUserProfiles");

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
                name: "IX_JobUserProfile_userProfileuserId",
                table: "JobUserProfile",
                column: "userProfileuserId");
        }
    }
}
