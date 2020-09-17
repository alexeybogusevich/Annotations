using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KNU.PR.DbManager.Migrations
{
    public partial class AddedClusterEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagNewsEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "ClusterId",
                table: "NewsEntity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClusterEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubclusterEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    ChildId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubclusterEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubclusterEntity_ClusterEntity_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ClusterEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubclusterEntity_ClusterEntity_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ClusterEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagClusterEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClusterId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false),
                    OccurencesCount = table.Column<int>(nullable: false),
                    NormOccurencesCount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagClusterEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagClusterEntity_ClusterEntity_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "ClusterEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagClusterEntity_TagEntity_TagId",
                        column: x => x.TagId,
                        principalTable: "TagEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsEntity_ClusterId",
                table: "NewsEntity",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_SubclusterEntity_ChildId",
                table: "SubclusterEntity",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_SubclusterEntity_ParentId",
                table: "SubclusterEntity",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TagClusterEntity_ClusterId",
                table: "TagClusterEntity",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_TagClusterEntity_TagId",
                table: "TagClusterEntity",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsEntity_ClusterEntity_ClusterId",
                table: "NewsEntity",
                column: "ClusterId",
                principalTable: "ClusterEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsEntity_ClusterEntity_ClusterId",
                table: "NewsEntity");

            migrationBuilder.DropTable(
                name: "SubclusterEntity");

            migrationBuilder.DropTable(
                name: "TagClusterEntity");

            migrationBuilder.DropTable(
                name: "ClusterEntity");

            migrationBuilder.DropIndex(
                name: "IX_NewsEntity_ClusterId",
                table: "NewsEntity");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "NewsEntity");

            migrationBuilder.CreateTable(
                name: "TagNewsEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurencesCount = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_TagNewsEntity_TagEntity_TagId",
                        column: x => x.TagId,
                        principalTable: "TagEntity",
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
    }
}
