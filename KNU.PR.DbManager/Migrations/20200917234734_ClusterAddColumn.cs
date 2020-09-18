using Microsoft.EntityFrameworkCore.Migrations;

namespace KNU.PR.DbManager.Migrations
{
    public partial class ClusterAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsCount",
                table: "ClusterEntity",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsCount",
                table: "ClusterEntity");
        }
    }
}
