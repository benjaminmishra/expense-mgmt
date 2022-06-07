using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseMgmt.Data.Migrations
{
    public partial class AdjustedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Approved by manager, Pending with accountant", "Pending to be paid" });

            migrationBuilder.UpdateData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Paid by accountant", "Paid" });

            migrationBuilder.UpdateData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Rejected by manager", "Rejected" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Approved by manager", "Approved" });

            migrationBuilder.UpdateData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Pending with accountant", "Pending to be paid" });

            migrationBuilder.UpdateData(
                table: "ExpenseStatusTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Paid by accountant", "Paid" });

            migrationBuilder.InsertData(
                table: "ExpenseStatusTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, "Rejected by manager", "Rejected" });
        }
    }
}
