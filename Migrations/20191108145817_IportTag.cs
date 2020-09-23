using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class IportTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "col_data",
                table: "mtd_cpq_import_param");

            migrationBuilder.AddColumn<string>(
                name: "tag_data",
                table: "mtd_cpq_import_param",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tag_data",
                table: "mtd_cpq_import_param");

            migrationBuilder.AddColumn<int>(
                name: "col_data",
                table: "mtd_cpq_import_param",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);
        }
    }
}
