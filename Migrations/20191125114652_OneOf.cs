using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class OneOf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "one_of",
                table: "mtd_cpq_rule_available",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tag_one_of",
                table: "mtd_cpq_import_param",
                type: "varchar(50)",
                nullable: false,
                defaultValueSql: "'&'");

            migrationBuilder.AddColumn<string>(
                name: "one_of",
                table: "mtd_cpq_import_data",
                type: "varchar(36)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "one_of",
                table: "mtd_cpq_rule_available");

            migrationBuilder.DropColumn(
                name: "tag_one_of",
                table: "mtd_cpq_import_param");

            migrationBuilder.DropColumn(
                name: "one_of",
                table: "mtd_cpq_import_data");
        }
    }
}
