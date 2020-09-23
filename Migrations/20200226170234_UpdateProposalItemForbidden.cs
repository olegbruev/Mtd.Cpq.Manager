using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateProposalItemForbidden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "forbidden",
                table: "mtd_cpq_proposal_item",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "forbidden",
                table: "mtd_cpq_proposal_item");
        }
    }
}
