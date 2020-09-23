using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class RuleRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type_config",
                table: "mtd_cpq_proposal");

            migrationBuilder.AddColumn<sbyte>(
                name: "config_change_rule",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<sbyte>(
                name: "config_master_included",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");

            migrationBuilder.AddColumn<sbyte>(
                name: "change_rule_relations",
                table: "mtd_cpq_group",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "config_change_rule",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "config_master_included",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "change_rule_relations",
                table: "mtd_cpq_group");

            migrationBuilder.AddColumn<int>(
                name: "type_config",
                table: "mtd_cpq_proposal",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'1'");
        }
    }
}
