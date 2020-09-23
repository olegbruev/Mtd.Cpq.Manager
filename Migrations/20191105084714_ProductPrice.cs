using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProductPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_product",
                type: "varchar(6048)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3027)");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "mtd_cpq_product",
                type: "decimal(20,2)",
                nullable: false,
                defaultValueSql: "'0'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "mtd_cpq_product");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_product",
                type: "varchar(3027)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6048)");
        }
    }
}
