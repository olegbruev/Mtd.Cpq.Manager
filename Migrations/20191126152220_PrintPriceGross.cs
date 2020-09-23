using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class PrintPriceGross : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "print_gross_price",
                table: "mtd_cpq_group",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "print_gross_price",
                table: "mtd_cpq_group");
        }
    }
}
