using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProposalShort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "title_note",
                table: "mtd_cpq_proposal",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "title_name",
                table: "mtd_cpq_proposal",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mtd_cpq_proposal",
                type: "varchar(3072)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3072)");

            migrationBuilder.AddColumn<sbyte>(
                name: "short",
                table: "mtd_cpq_proposal",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "short",
                table: "mtd_cpq_proposal");

            migrationBuilder.AlterColumn<string>(
                name: "title_note",
                table: "mtd_cpq_proposal",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title_name",
                table: "mtd_cpq_proposal",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mtd_cpq_proposal",
                type: "varchar(3072)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3072)",
                oldNullable: true);
        }
    }
}
