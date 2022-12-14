using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedpasswordtouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_CreatedBy",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_UpdatedBy",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Users_CreatedBy",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Users_UpdatedBy",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Users_CreatedBy",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Users_UpdatedBy",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Users_CreatedBy",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Users_UpdatedBy",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_Users_CreatedBy",
                table: "BookIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_Users_UpdatedBy",
                table: "BookIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_CreatedBy",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_UpdatedBy",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_CreatedBy",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_UpdatedBy",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CreatedBy",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UpdatedBy",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CreatedBy",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UpdatedBy",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Genres_CreatedBy",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_UpdatedBy",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CreatedBy",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_UpdatedBy",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_BookIssues_CreatedBy",
                table: "BookIssues");

            migrationBuilder.DropIndex(
                name: "IX_BookIssues_UpdatedBy",
                table: "BookIssues");

            migrationBuilder.DropIndex(
                name: "IX_BookGenres_CreatedBy",
                table: "BookGenres");

            migrationBuilder.DropIndex(
                name: "IX_BookGenres_UpdatedBy",
                table: "BookGenres");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthors_CreatedBy",
                table: "BookAuthors");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthors_UpdatedBy",
                table: "BookAuthors");

            migrationBuilder.DropIndex(
                name: "IX_Book_CreatedBy",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_UpdatedBy",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CreatedBy",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_UpdatedBy",
                table: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 12, 2, 9, 7, 3, 387, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Book_BookId",
                table: "BookGenres",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Book_BookId",
                table: "BookGenres");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 30, 8, 43, 44, 465, DateTimeKind.Local).AddTicks(6944));

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedBy",
                table: "Users",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedBy",
                table: "Users",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_CreatedBy",
                table: "Genres",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_UpdatedBy",
                table: "Genres",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CreatedBy",
                table: "Departments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UpdatedBy",
                table: "Departments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_CreatedBy",
                table: "BookIssues",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_UpdatedBy",
                table: "BookIssues",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_CreatedBy",
                table: "BookGenres",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_UpdatedBy",
                table: "BookGenres",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_CreatedBy",
                table: "BookAuthors",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_UpdatedBy",
                table: "BookAuthors",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CreatedBy",
                table: "Book",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Book_UpdatedBy",
                table: "Book",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CreatedBy",
                table: "Authors",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UpdatedBy",
                table: "Authors",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_CreatedBy",
                table: "Authors",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_UpdatedBy",
                table: "Authors",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Users_CreatedBy",
                table: "Book",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Users_UpdatedBy",
                table: "Book",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Users_CreatedBy",
                table: "BookAuthors",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Users_UpdatedBy",
                table: "BookAuthors",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Users_CreatedBy",
                table: "BookGenres",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Users_UpdatedBy",
                table: "BookGenres",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_Users_CreatedBy",
                table: "BookIssues",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_Users_UpdatedBy",
                table: "BookIssues",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_CreatedBy",
                table: "Departments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_UpdatedBy",
                table: "Departments",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_CreatedBy",
                table: "Genres",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_UpdatedBy",
                table: "Genres",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CreatedBy",
                table: "Users",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UpdatedBy",
                table: "Users",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
