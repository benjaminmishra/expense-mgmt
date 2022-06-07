using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseMgmt.Data.Migrations
{
    public partial class NewStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExpenseStatusTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, "Rejected by manager", "Rejected" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
