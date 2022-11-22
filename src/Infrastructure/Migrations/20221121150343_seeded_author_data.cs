using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seededauthordata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "AuthorName", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "J. K. Rowling", 1, new DateTime(2022, 11, 21, 10, 3, 42, 904, DateTimeKind.Local).AddTicks(174), null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
