using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ImportParam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "import_tag",
                table: "mtd_cpq_catalog",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mtd_cpq_import_param",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    col_data = table.Column<string>(type: "varchar(50)", nullable: false),
                    col_number = table.Column<string>(type: "varchar(50)", nullable: false),
                    col_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    col_price = table.Column<string>(type: "varchar(50)", nullable: true),
                    tag_master = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_import_param", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_import_param",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_import_param");

            migrationBuilder.DropColumn(
                name: "import_tag",
                table: "mtd_cpq_catalog");
        }
    }
}
