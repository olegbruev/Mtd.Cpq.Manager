using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProposalItemsAnchor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "anchor_history",
                table: "mtd_cpq_proposal_item",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<string>(
                name: "anchor_notice",
                table: "mtd_cpq_proposal_item",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "anchor_history",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "anchor_notice",
                table: "mtd_cpq_proposal_item");
        }
    }
}
