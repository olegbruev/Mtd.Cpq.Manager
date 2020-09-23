using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class DataSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tag_one_of",
                table: "mtd_cpq_import_param");

            migrationBuilder.AddColumn<string>(
                name: "datasheet",
                table: "mtd_cpq_proposal_item",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "master_note",
                table: "mtd_cpq_proposal",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6048)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "master_datasheet",
                table: "mtd_cpq_proposal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "datasheet",
                table: "mtd_cpq_product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "col_data",
                table: "mtd_cpq_import_param",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datasheet",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "master_datasheet",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "datasheet",
                table: "mtd_cpq_product");

            migrationBuilder.DropColumn(
                name: "col_data",
                table: "mtd_cpq_import_param");

            migrationBuilder.AlterColumn<string>(
                name: "master_note",
                table: "mtd_cpq_proposal",
                type: "varchar(6048)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tag_one_of",
                table: "mtd_cpq_import_param",
                type: "varchar(50)",
                nullable: false,
                defaultValueSql: "'&'");
        }
    }
}
