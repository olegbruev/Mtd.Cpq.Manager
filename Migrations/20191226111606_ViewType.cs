using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ViewType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "view_datasheet",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<sbyte>(
                name: "view_proposal",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "view_datasheet",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "view_proposal",
                table: "mtd_cpq_proposal");
        }
    }
}
