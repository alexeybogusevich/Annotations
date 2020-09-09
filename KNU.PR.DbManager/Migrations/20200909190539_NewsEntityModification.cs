using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KNU.PR.DbManager.Migrations
{
    public partial class NewsEntityModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagNewsEntity_Tag_TagId",
                table: "TagNewsEntity");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropColumn(
                name: "TagOccurencesCount",
                table: "TagNewsEntity");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "NewsEntity");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "NewsEntity",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "OccurencesCount",
                table: "TagNewsEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "NewsEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "NewsEntity",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "NewsEntity",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SourceName",
                table: "NewsEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NewsEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "NewsEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlToImage",
                table: "NewsEntity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TagEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntity", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TagNewsEntity_TagEntity_TagId",
                table: "TagNewsEntity",
                column: "TagId",
                principalTable: "TagEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagNewsEntity_TagEntity_TagId",
                table: "TagNewsEntity");

            migrationBuilder.DropTable(
                name: "TagEntity");

            migrationBuilder.DropColumn(
                name: "OccurencesCount",
                table: "TagNewsEntity");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "NewsEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "NewsEntity");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "NewsEntity");

            migrationBuilder.DropColumn(
                name: "SourceName",
                table: "NewsEntity");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "NewsEntity");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "NewsEntity");

            migrationBuilder.DropColumn(
                name: "UrlToImage",
                table: "NewsEntity");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "NewsEntity",
                newName: "Text");

            migrationBuilder.AddColumn<int>(
                name: "TagOccurencesCount",
                table: "TagNewsEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "NewsEntity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TagNewsEntity_Tag_TagId",
                table: "TagNewsEntity",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
