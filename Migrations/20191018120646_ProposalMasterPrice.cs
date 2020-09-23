using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProposalMasterPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "master-price",
                table: "mtd_cpq_proposal",
                type: "decimal(20,2)",
                nullable: false,
                defaultValueSql: "'0.00'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "master-price",
                table: "mtd_cpq_proposal");
        }
    }
}
