using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseMgmt.Data.Migrations
{
    public partial class AddEmployeeRoletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Employees",
                newName: "RoleIdId");

            migrationBuilder.CreateTable(
                name: "EmployeeRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleIdId",
                table: "Employees",
                column: "RoleIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRole_RoleIdId",
                table: "Employees",
                column: "RoleIdId",
                principalTable: "EmployeeRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRole_RoleIdId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeRole");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RoleIdId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "RoleIdId",
                table: "Employees",
                newName: "RoleId");
        }
    }
}
