using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Migrations
{
    public partial class doctors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiseaseDoctor");

            migrationBuilder.DropTable(  
                name: "DiseasePatient");

            migrationBuilder.DropTable(
                name: "DoctorPatient");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Patients",
                newName: "Disease");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "Id"); 

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Doctors",
                newName: "Specialty");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Doctors",
                newName: "Id");
             
            migrationBuilder.RenameColumn(
                name: "DiseaseId",
                table: "Diseases",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "AdmittedDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DischargeDate",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AdmittedDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DischargeDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Disease",
                table: "Patients",
                newName: "ContactNumber");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patients",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "Specialty",
                table: "Doctors",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Doctors",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Diseases",
                newName: "DiseaseId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DiseaseDoctor",
                columns: table => new
                {
                    DiseasesDiseaseId = table.Column<int>(type: "int", nullable: false),
                    DoctorsDoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseDoctor", x => new { x.DiseasesDiseaseId, x.DoctorsDoctorId });
                    table.ForeignKey(
                        name: "FK_DiseaseDoctor_Diseases_DiseasesDiseaseId",
                        column: x => x.DiseasesDiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseaseDoctor_Doctors_DoctorsDoctorId",
                        column: x => x.DoctorsDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiseasePatient",
                columns: table => new
                {
                    DiseasesDiseaseId = table.Column<int>(type: "int", nullable: false),
                    PatientsPatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseasePatient", x => new { x.DiseasesDiseaseId, x.PatientsPatientId });
                    table.ForeignKey(
                        name: "FK_DiseasePatient_Diseases_DiseasesDiseaseId",
                        column: x => x.DiseasesDiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseasePatient_Patients_PatientsPatientId",
                        column: x => x.PatientsPatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorPatient",
                columns: table => new
                {
                    DoctorsDoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientsPatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatient", x => new { x.DoctorsDoctorId, x.PatientsPatientId });
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Doctors_DoctorsDoctorId",
                        column: x => x.DoctorsDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Patients_PatientsPatientId",
                        column: x => x.PatientsPatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseDoctor_DoctorsDoctorId",
                table: "DiseaseDoctor",
                column: "DoctorsDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasePatient_PatientsPatientId",
                table: "DiseasePatient",
                column: "PatientsPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatient_PatientsPatientId",
                table: "DoctorPatient",
                column: "PatientsPatientId");
        }
    }
}
