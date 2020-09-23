using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ImportDataNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MtdCpqImportData_Note",
                table: "mtd_cpq_import_data");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "mtd_cpq_import_data",
                newName: "note");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "mtd_cpq_import_data",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "mtd_cpq_import_data");

            migrationBuilder.RenameColumn(
                name: "note",
                table: "mtd_cpq_import_data",
                newName: "Note");

            migrationBuilder.AddColumn<string>(
                name: "MtdCpqImportData_Note",
                table: "mtd_cpq_import_data",
                nullable: true);
        }
    }
}
