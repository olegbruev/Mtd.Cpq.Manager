using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProductNoteNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_product",
                type: "varchar(6048)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6048)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_product",
                type: "varchar(6048)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6048)",
                oldNullable: true);
        }
    }
}
