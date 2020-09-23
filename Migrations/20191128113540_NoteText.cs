using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class NoteText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_proposal_item",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6048)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6048)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_import_data",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6048)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_proposal_item",
                type: "varchar(6048)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_product",
                type: "varchar(6048)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_import_data",
                type: "varchar(6048)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
