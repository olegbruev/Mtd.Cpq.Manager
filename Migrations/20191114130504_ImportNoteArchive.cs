using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ImportNoteArchive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "note_load",
                table: "mtd_cpq_import",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<sbyte>(
                name: "old_to_archive",
                table: "mtd_cpq_import",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "note_load",
                table: "mtd_cpq_import");

            migrationBuilder.DropColumn(
                name: "old_to_archive",
                table: "mtd_cpq_import");
        }
    }
}
