using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccsess.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeEmpId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_EmployeeEmpId",
                table: "Skills",
                column: "EmployeeEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Employees_EmployeeEmpId",
                table: "Skills",
                column: "EmployeeEmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Employees_EmployeeEmpId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_EmployeeEmpId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpId",
                table: "Skills");
        }
    }
}
