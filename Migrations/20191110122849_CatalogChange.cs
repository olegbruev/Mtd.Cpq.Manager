using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class CatalogChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mtd_cpq_catalog_mtd_cpq_catalog_ParentNavigationId",
                table: "mtd_cpq_catalog");

            migrationBuilder.DropIndex(
                name: "IX_mtd_cpq_catalog_ParentNavigationId",
                table: "mtd_cpq_catalog");

            migrationBuilder.DropColumn(
                name: "ParentNavigationId",
                table: "mtd_cpq_catalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentNavigationId",
                table: "mtd_cpq_catalog",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_mtd_cpq_catalog_ParentNavigationId",
                table: "mtd_cpq_catalog",
                column: "ParentNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_mtd_cpq_catalog_mtd_cpq_catalog_ParentNavigationId",
                table: "mtd_cpq_catalog",
                column: "ParentNavigationId",
                principalTable: "mtd_cpq_catalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
