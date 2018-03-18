using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DocRepoApi.Migrations
{
    public partial class ClientCatalogSeparateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientCatalog",
                table: "Document");

            migrationBuilder.CreateTable(
                name: "ClientCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CatalogId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCatalog_ClientCatalog_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "ClientCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentCatalog_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCatalog_CatalogId",
                table: "DocumentCatalog",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCatalog_DocumentId",
                table: "DocumentCatalog",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentCatalog");

            migrationBuilder.DropTable(
                name: "ClientCatalog");

            migrationBuilder.AddColumn<string>(
                name: "ClientCatalog",
                table: "Document",
                nullable: true);
        }
    }
}
