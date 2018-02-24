using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DocRepoApi.Migrations
{
    public partial class AnnotationCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentUpdate_LatestUpdateId1",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_LatestUpdateId1",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "LatestUpdateId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "LatestUpdateId1",
                table: "Document");

            migrationBuilder.RenameColumn(
                name: "FitForClients",
                table: "Document",
                newName: "IsFitForClients");

            migrationBuilder.AlterColumn<string>(
                name: "Release",
                table: "ProductVersion",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Product",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "DocumentType",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "AitId",
                table: "Document",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Author",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Author",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                table: "Author",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "IsFormerAuthor",
                table: "Author",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFormerAuthor",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "IsFitForClients",
                table: "Document",
                newName: "FitForClients");

            migrationBuilder.AlterColumn<string>(
                name: "Release",
                table: "ProductVersion",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Product",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "DocumentType",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<int>(
                name: "AitId",
                table: "Document",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LatestUpdateId",
                table: "Document",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LatestUpdateId1",
                table: "Document",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Author",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Author",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                table: "Author",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 7);

            migrationBuilder.CreateIndex(
                name: "IX_Document_LatestUpdateId1",
                table: "Document",
                column: "LatestUpdateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentUpdate_LatestUpdateId1",
                table: "Document",
                column: "LatestUpdateId1",
                principalTable: "DocumentUpdate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
