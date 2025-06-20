using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerGuidancePlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddPhaseSubstepsNavProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoadmapProgresses_RoadmapSteps_RoadmapStepId",
                table: "UserRoadmapProgresses");

            migrationBuilder.RenameColumn(
                name: "RoadmapStepId",
                table: "UserRoadmapProgresses",
                newName: "SubstepId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoadmapProgresses_RoadmapStepId",
                table: "UserRoadmapProgresses",
                newName: "IX_UserRoadmapProgresses_SubstepId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoadmapProgresses_phase_substeps_SubstepId",
                table: "UserRoadmapProgresses",
                column: "SubstepId",
                principalTable: "phase_substeps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoadmapProgresses_phase_substeps_SubstepId",
                table: "UserRoadmapProgresses");

            migrationBuilder.RenameColumn(
                name: "SubstepId",
                table: "UserRoadmapProgresses",
                newName: "RoadmapStepId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoadmapProgresses_SubstepId",
                table: "UserRoadmapProgresses",
                newName: "IX_UserRoadmapProgresses_RoadmapStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoadmapProgresses_RoadmapSteps_RoadmapStepId",
                table: "UserRoadmapProgresses",
                column: "RoadmapStepId",
                principalTable: "RoadmapSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
