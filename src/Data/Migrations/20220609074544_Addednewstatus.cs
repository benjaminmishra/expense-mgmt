using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseMgmt.Data.Migrations
{
    public partial class Addednewstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FullName", "IsActive", "ManagerId", "Password", "RoleId" },
                values: new object[] { 5, "Employee Joe", true, 6, "xyz", 1 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FullName", "IsActive", "ManagerId", "Password", "RoleId" },
                values: new object[] { 6, "Manager Doe", true, 0, "abc", 2 });

            migrationBuilder.InsertData(
                table: "ExpenseStatusTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, "Sent back by manager", "Review" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
