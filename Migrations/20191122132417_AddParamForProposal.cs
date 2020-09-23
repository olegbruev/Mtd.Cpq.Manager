using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class AddParamForProposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mtd_cpq_group_param_id",
                table: "mtd_cpq_proposal",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposal_group_param_idx",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_param_id");

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_group_param",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_param_id",
                principalTable: "mtd_cpq_group_param",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_group_param",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropIndex(
                name: "fk_mtd_cpq_proposal_group_param_idx",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "mtd_cpq_group_param_id",
                table: "mtd_cpq_proposal");
        }
    }
}
