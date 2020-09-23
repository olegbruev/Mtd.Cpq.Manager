using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ViewQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price_gross",
                table: "mtd_cpq_proposal");

            migrationBuilder.RenameColumn(
                name: "view_price_cusomer",
                table: "mtd_cpq_proposal",
                newName: "view_price_customer");

            migrationBuilder.AlterColumn<sbyte>(
                name: "view_note",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'1'");

            migrationBuilder.AddColumn<sbyte>(
                name: "view_qty",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "view_qty",
                table: "mtd_cpq_proposal");

            migrationBuilder.RenameColumn(
                name: "view_price_customer",
                table: "mtd_cpq_proposal",
                newName: "view_price_cusomer");

            migrationBuilder.AlterColumn<sbyte>(
                name: "view_note",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "price_gross",
                table: "mtd_cpq_proposal",
                type: "decimal(20,2)",
                nullable: false,
                defaultValueSql: "'0.00'");
        }
    }
}
