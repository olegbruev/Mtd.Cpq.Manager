using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ColInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "col_price",
                table: "mtd_cpq_import_param",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "col_number",
                table: "mtd_cpq_import_param",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<int>(
                name: "col_name",
                table: "mtd_cpq_import_param",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<int>(
                name: "col_data",
                table: "mtd_cpq_import_param",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "col_price",
                table: "mtd_cpq_import_param",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<string>(
                name: "col_number",
                table: "mtd_cpq_import_param",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<string>(
                name: "col_name",
                table: "mtd_cpq_import_param",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<string>(
                name: "col_data",
                table: "mtd_cpq_import_param",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");
        }
    }
}
