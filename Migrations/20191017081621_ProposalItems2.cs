using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProposalItems2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "required",
                table: "mtd_cpq_proposal_item",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<sbyte>(
                name: "selected",
                table: "mtd_cpq_proposal_item",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");

            migrationBuilder.CreateIndex(
                name: "IX_REQUIRED",
                table: "mtd_cpq_proposal_item",
                column: "required");

            migrationBuilder.CreateIndex(
                name: "IX_SELECTED",
                table: "mtd_cpq_proposal_item",
                column: "selected");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_REQUIRED",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropIndex(
                name: "IX_SELECTED",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "required",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "selected",
                table: "mtd_cpq_proposal_item");
        }
    }
}
