using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DocRepoApi.Migrations
{
    public partial class AddRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentAuthor_AuthorId",
                table: "DocumentAuthor");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DocumentUpdate",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Document",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AitName",
                table: "Author",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAuthor_AuthorId",
                table: "DocumentAuthor",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentAuthor_AuthorId",
                table: "DocumentAuthor");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DocumentUpdate");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "AitName",
                table: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAuthor_AuthorId",
                table: "DocumentAuthor",
                column: "AuthorId",
                unique: true);
        }
    }
}
