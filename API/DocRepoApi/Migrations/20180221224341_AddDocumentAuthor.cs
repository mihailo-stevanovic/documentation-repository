using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DocRepoApi.Migrations
{
    public partial class AddDocumentAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Document_DocumentId",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_DocumentId",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Author");

            migrationBuilder.CreateTable(
                name: "DocumentAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAuthor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAuthor_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAuthor_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAuthor_AuthorId",
                table: "DocumentAuthor",
                column: "AuthorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAuthor_DocumentId",
                table: "DocumentAuthor",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAuthor");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Author",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_DocumentId",
                table: "Author",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Document_DocumentId",
                table: "Author",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
