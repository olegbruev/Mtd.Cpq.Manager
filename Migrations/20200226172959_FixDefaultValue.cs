using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class FixDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "required",
                table: "mtd_cpq_rule_anchor_bind",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'0'");

            migrationBuilder.AlterColumn<sbyte>(
                name: "include",
                table: "mtd_cpq_rule_anchor_bind",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<sbyte>(
                name: "required",
                table: "mtd_cpq_proposal_anchor",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'0'");

            migrationBuilder.AlterColumn<sbyte>(
                name: "include",
                table: "mtd_cpq_proposal_anchor",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "required",
                table: "mtd_cpq_rule_anchor_bind",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "include",
                table: "mtd_cpq_rule_anchor_bind",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "required",
                table: "mtd_cpq_proposal_anchor",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "include",
                table: "mtd_cpq_proposal_anchor",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");
        }
    }
}
