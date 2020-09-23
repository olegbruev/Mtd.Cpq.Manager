using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class MasterProposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "master_product_id",
                table: "mtd_cpq_proposal",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_mtd_cpq_proposal_master_product_id",
                table: "mtd_cpq_proposal",
                column: "master_product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_product",
                table: "mtd_cpq_proposal",
                column: "master_product_id",
                principalTable: "mtd_cpq_product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_product",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropIndex(
                name: "IX_mtd_cpq_proposal_master_product_id",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "master_product_id",
                table: "mtd_cpq_proposal");
        }
    }
}
