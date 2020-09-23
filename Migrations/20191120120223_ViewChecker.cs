using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ViewChecker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "short",
                table: "mtd_cpq_proposal",
                newName: "view_price_gross");

            migrationBuilder.RenameColumn(
                name: "not_price",
                table: "mtd_cpq_proposal",
                newName: "view_price_cusomer");

            migrationBuilder.AddColumn<sbyte>(
                name: "view_delivery",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");

            migrationBuilder.AddColumn<sbyte>(
                name: "view_note",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "view_delivery",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "view_note",
                table: "mtd_cpq_proposal");

            migrationBuilder.RenameColumn(
                name: "view_price_gross",
                table: "mtd_cpq_proposal",
                newName: "short");

            migrationBuilder.RenameColumn(
                name: "view_price_cusomer",
                table: "mtd_cpq_proposal",
                newName: "not_price");
        }
    }
}
