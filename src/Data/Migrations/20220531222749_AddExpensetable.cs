using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseMgmt.Data.Migrations
{
    public partial class AddExpensetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRole_RoleIdId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRole",
                table: "EmployeeRole");

            migrationBuilder.RenameTable(
                name: "EmployeeRole",
                newName: "EmployeeRoles");

            migrationBuilder.RenameColumn(
                name: "RoleIdId",
                table: "Employees",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_RoleIdId",
                table: "Employees",
                newName: "IX_Employees_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Employees_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncurredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ExpenseId",
                table: "Bills",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CreatedBy",
                table: "Expenses",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRoles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "EmployeeRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRoles_RoleId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles");

            migrationBuilder.RenameTable(
                name: "EmployeeRoles",
                newName: "EmployeeRole");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Employees",
                newName: "RoleIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                newName: "IX_Employees_RoleIdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRole",
                table: "EmployeeRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRole_RoleIdId",
                table: "Employees",
                column: "RoleIdId",
                principalTable: "EmployeeRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
