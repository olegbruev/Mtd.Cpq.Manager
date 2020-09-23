using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UniqueDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_number",
                table: "mtd_cpq_catalog");

            migrationBuilder.AlterColumn<string>(
                name: "contact_name",
                table: "mtd_cpq_group_param",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "contact_email",
                table: "mtd_cpq_group_param",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_group",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_catalog",
                type: "varchar(768)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(768)");

            migrationBuilder.CreateIndex(
                name: "ix_number",
                table: "mtd_cpq_catalog",
                column: "id_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_number",
                table: "mtd_cpq_catalog");

            migrationBuilder.AlterColumn<string>(
                name: "contact_name",
                table: "mtd_cpq_group_param",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "contact_email",
                table: "mtd_cpq_group_param",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_group",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mtd_cpq_catalog",
                type: "varchar(768)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(768)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_number",
                table: "mtd_cpq_catalog",
                column: "id_number",
                unique: true);
        }
    }
}
