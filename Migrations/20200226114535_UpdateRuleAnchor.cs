using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateRuleAnchor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "include",
                table: "mtd_cpq_rule_anchor_bind",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");

            migrationBuilder.AddColumn<sbyte>(
                name: "required",
                table: "mtd_cpq_rule_anchor_bind",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<sbyte>(
                name: "include",
                table: "mtd_cpq_proposal_anchor",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");

            migrationBuilder.AddColumn<sbyte>(
                name: "required",
                table: "mtd_cpq_proposal_anchor",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "include",
                table: "mtd_cpq_rule_anchor_bind");

            migrationBuilder.DropColumn(
                name: "required",
                table: "mtd_cpq_rule_anchor_bind");

            migrationBuilder.DropColumn(
                name: "include",
                table: "mtd_cpq_proposal_anchor");

            migrationBuilder.DropColumn(
                name: "required",
                table: "mtd_cpq_proposal_anchor");
        }
    }
}
