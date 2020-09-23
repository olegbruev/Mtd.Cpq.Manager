using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class BigUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "col_price",
                table: "mtd_cpq_import_param",
                newName: "cell_price");

            migrationBuilder.RenameColumn(
                name: "col_number",
                table: "mtd_cpq_import_param",
                newName: "cell_number");

            migrationBuilder.RenameColumn(
                name: "col_name",
                table: "mtd_cpq_import_param",
                newName: "cell_note");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "mtd_cpq_import_data",
                newName: "Note");

            migrationBuilder.AddColumn<sbyte>(
                name: "archive",
                table: "mtd_cpq_product",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<int>(
                name: "cell_name",
                table: "mtd_cpq_import_param",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "mtd_cpq_import_data",
                type: "varchar(6048)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<string>(
                name: "MtdCpqImportData_Note",
                table: "mtd_cpq_import_data",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_archive",
                table: "mtd_cpq_product",
                column: "archive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_archive",
                table: "mtd_cpq_product");

            migrationBuilder.DropColumn(
                name: "archive",
                table: "mtd_cpq_product");

            migrationBuilder.DropColumn(
                name: "cell_name",
                table: "mtd_cpq_import_param");

            migrationBuilder.DropColumn(
                name: "MtdCpqImportData_Note",
                table: "mtd_cpq_import_data");

            migrationBuilder.RenameColumn(
                name: "cell_price",
                table: "mtd_cpq_import_param",
                newName: "col_price");

            migrationBuilder.RenameColumn(
                name: "cell_number",
                table: "mtd_cpq_import_param",
                newName: "col_number");

            migrationBuilder.RenameColumn(
                name: "cell_note",
                table: "mtd_cpq_import_param",
                newName: "col_name");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "mtd_cpq_import_data",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mtd_cpq_import_data",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6048)");
        }
    }
}
