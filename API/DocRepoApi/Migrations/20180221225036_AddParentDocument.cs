using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DocRepoApi.Migrations
{
    public partial class AddParentDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentDocumentId",
                table: "Document",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Document_ParentDocumentId",
                table: "Document",
                column: "ParentDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Document_ParentDocumentId",
                table: "Document",
                column: "ParentDocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Document_ParentDocumentId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_ParentDocumentId",
                table: "Document");

            migrationBuilder.AlterColumn<int>(
                name: "ParentDocumentId",
                table: "Document",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
