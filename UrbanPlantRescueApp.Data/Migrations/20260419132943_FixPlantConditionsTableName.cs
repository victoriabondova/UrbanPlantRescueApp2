using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanPlantRescueApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPlantConditionsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantsConditions_Plants_PlantId",
                table: "PlantsConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantsConditions",
                table: "PlantsConditions");

            migrationBuilder.RenameTable(
                name: "PlantsConditions",
                newName: "PlantConditions");

            migrationBuilder.RenameIndex(
                name: "IX_PlantsConditions_PlantId",
                table: "PlantConditions",
                newName: "IX_PlantConditions_PlantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantConditions",
                table: "PlantConditions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantConditions_Plants_PlantId",
                table: "PlantConditions",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantConditions_Plants_PlantId",
                table: "PlantConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantConditions",
                table: "PlantConditions");

            migrationBuilder.RenameTable(
                name: "PlantConditions",
                newName: "PlantsConditions");

            migrationBuilder.RenameIndex(
                name: "IX_PlantConditions_PlantId",
                table: "PlantsConditions",
                newName: "IX_PlantsConditions_PlantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantsConditions",
                table: "PlantsConditions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantsConditions_Plants_PlantId",
                table: "PlantsConditions",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
