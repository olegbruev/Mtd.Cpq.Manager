using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class SelectedDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "selected",
                table: "mtd_cpq_proposal_item",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "selected",
                table: "mtd_cpq_proposal_item",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");
        }
    }
}
