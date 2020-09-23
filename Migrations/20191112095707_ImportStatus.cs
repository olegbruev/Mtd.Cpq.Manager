using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ImportStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status_error",
                table: "mtd_cpq_import",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status_process",
                table: "mtd_cpq_import",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<string>(
                name: "status_text",
                table: "mtd_cpq_import",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "idx_process",
                table: "mtd_cpq_import",
                column: "status_process");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_process",
                table: "mtd_cpq_import");

            migrationBuilder.DropColumn(
                name: "status_error",
                table: "mtd_cpq_import");

            migrationBuilder.DropColumn(
                name: "status_process",
                table: "mtd_cpq_import");

            migrationBuilder.DropColumn(
                name: "status_text",
                table: "mtd_cpq_import");
        }
    }
}
