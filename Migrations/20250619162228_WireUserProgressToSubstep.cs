using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerGuidancePlatform.Migrations
{
    /// <inheritdoc />
    public partial class WireUserProgressToSubstep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoadmapProgresses_phase_substeps_SubstepId",
                table: "UserRoadmapProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserRoadmapProgresses_SubstepId",
                table: "UserRoadmapProgresses");

            migrationBuilder.DropColumn(
                name: "SubstepId",
                table: "UserRoadmapProgresses");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoadmapProgresses_StepId",
                table: "UserRoadmapProgresses",
                column: "StepId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoadmapProgresses_phase_substeps_StepId",
                table: "UserRoadmapProgresses",
                column: "StepId",
                principalTable: "phase_substeps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoadmapProgresses_phase_substeps_StepId",
                table: "UserRoadmapProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserRoadmapProgresses_StepId",
                table: "UserRoadmapProgresses");

            migrationBuilder.AddColumn<int>(
                name: "SubstepId",
                table: "UserRoadmapProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoadmapProgresses_SubstepId",
                table: "UserRoadmapProgresses",
                column: "SubstepId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoadmapProgresses_phase_substeps_SubstepId",
                table: "UserRoadmapProgresses",
                column: "SubstepId",
                principalTable: "phase_substeps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
