using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProposalCatalogId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "mtd_cpq_proposal_catalog_id",
                table: "mtd_cpq_proposal_item",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "mtd_cpq_proposal_catalog_id",
                table: "mtd_cpq_proposal_item",
                type: "varchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(36)");
        }
    }
}
