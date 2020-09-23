using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class AddParamForProposal2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update mtd_cpq_proposal as p inner join " +
                "(SELECT id, (select id from mtd_cpq_group_param where mtd_cpq_config_id = s.mtd_cpq_group_id Limit 1) as param " +
                "FROM  mtd_cpq_proposal as s) as p1 on p.id = p1.id set p.mtd_cpq_group_param_id = p1.param; ");

            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_group_param",
                table: "mtd_cpq_proposal");

            migrationBuilder.AlterColumn<string>(
                name: "mtd_cpq_group_param_id",
                table: "mtd_cpq_proposal",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_id",
                principalTable: "mtd_cpq_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_group_param",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_param_id",
                principalTable: "mtd_cpq_group_param",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_group_param",
                table: "mtd_cpq_proposal");

            migrationBuilder.AlterColumn<string>(
                name: "mtd_cpq_group_param_id",
                table: "mtd_cpq_proposal",
                type: "varchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_id",
                principalTable: "mtd_cpq_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_group_param",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_param_id",
                principalTable: "mtd_cpq_group_param",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
