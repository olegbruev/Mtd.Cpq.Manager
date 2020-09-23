using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ImportDataSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "datasheet",
                table: "mtd_cpq_import_data",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<sbyte>(
                name: "datasheet_load",
                table: "mtd_cpq_import",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datasheet",
                table: "mtd_cpq_import_data");

            migrationBuilder.DropColumn(
                name: "datasheet_load",
                table: "mtd_cpq_import");
        }
    }
}
