using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateTitleSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "logo_height",
                table: "mtd_cpq_titles",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<int>(
                name: "logo_width",
                table: "mtd_cpq_titles",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<int>(
                name: "logo_height",
                table: "mtd_cpq_proposal",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<int>(
                name: "logo_width",
                table: "mtd_cpq_proposal",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "logo_height",
                table: "mtd_cpq_titles");

            migrationBuilder.DropColumn(
                name: "logo_width",
                table: "mtd_cpq_titles");

            migrationBuilder.DropColumn(
                name: "logo_height",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "logo_width",
                table: "mtd_cpq_proposal");
        }
    }
}
