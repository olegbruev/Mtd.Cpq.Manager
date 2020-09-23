using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class Import : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mtd_cpq_import",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    time_cr = table.Column<DateTime>(type: "datetime", nullable: false),
                    user_id = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_import", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_import_data",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    mtd_cpq_import_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(20,2)", nullable: false, defaultValueSql: "'0.00'"),
                    required = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    parent = table.Column<string>(type: "varchar(45)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_import_data", x => x.id);
                    table.ForeignKey(
                        name: "fk_import_data_history",
                        column: x => x.mtd_cpq_import_id,
                        principalTable: "mtd_cpq_import",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_import",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_import_data",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_number",
                table: "mtd_cpq_import_data",
                column: "id_number");

            migrationBuilder.CreateIndex(
                name: "fk_import_data_history_idx",
                table: "mtd_cpq_import_data",
                column: "mtd_cpq_import_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_import_data");

            migrationBuilder.DropTable(
                name: "mtd_cpq_import");
        }
    }
}
