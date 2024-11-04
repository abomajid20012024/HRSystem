using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_table_SalaryTiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SalaryTiersId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SalaryTiers",
                columns: table => new
                {
                    SalaryTiersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseSalary = table.Column<float>(type: "real", nullable: false),
                    Bonus = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryTiers", x => x.SalaryTiersId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SalaryTiersId",
                table: "Employees",
                column: "SalaryTiersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SalaryTiers_SalaryTiersId",
                table: "Employees",
                column: "SalaryTiersId",
                principalTable: "SalaryTiers",
                principalColumn: "SalaryTiersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SalaryTiers_SalaryTiersId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "SalaryTiers");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SalaryTiersId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalaryTiersId",
                table: "Employees");
        }
    }
}
