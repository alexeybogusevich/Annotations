using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KNU.PR.DbManager.Migrations
{
    public partial class ModelsCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagNewsEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NewsEntityId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false),
                    TagOccurencesCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagNewsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagNewsEntity_NewsEntity_NewsEntityId",
                        column: x => x.NewsEntityId,
                        principalTable: "NewsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagNewsEntity_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagNewsEntity_NewsEntityId",
                table: "TagNewsEntity",
                column: "NewsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TagNewsEntity_TagId",
                table: "TagNewsEntity",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagNewsEntity");

            migrationBuilder.DropTable(
                name: "NewsEntity");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
