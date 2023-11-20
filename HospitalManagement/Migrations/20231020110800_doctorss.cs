using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Migrations
{
    public partial class doctorss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disease",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "DiseaseId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DiseaseId",
                table: "Patients",
                column: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Diseases_DiseaseId",
                table: "Patients",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Diseases_DiseaseId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DiseaseId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "Disease",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
