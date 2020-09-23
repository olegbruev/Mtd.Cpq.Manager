using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class AddMasterFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                newName: "col_note");

            migrationBuilder.RenameColumn(
                name: "cell_name",
                table: "mtd_cpq_import_param",
                newName: "col_name");

            migrationBuilder.AddColumn<sbyte>(
                name: "master_product",
                table: "mtd_cpq_import_data",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "master_product",
                table: "mtd_cpq_import_data");

            migrationBuilder.RenameColumn(
                name: "col_price",
                table: "mtd_cpq_import_param",
                newName: "cell_price");

            migrationBuilder.RenameColumn(
                name: "col_number",
                table: "mtd_cpq_import_param",
                newName: "cell_number");

            migrationBuilder.RenameColumn(
                name: "col_note",
                table: "mtd_cpq_import_param",
                newName: "cell_note");

            migrationBuilder.RenameColumn(
                name: "col_name",
                table: "mtd_cpq_import_param",
                newName: "cell_name");
        }
    }
}
