using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class AddTtilesCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mtd_cpq_counter",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    id_number = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_counter", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_titles",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    logo = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    prepared_by = table.Column<string>(type: "varchar(255)", nullable: false),
                    contact_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    contact_phone = table.Column<string>(type: "varchar(255)", nullable: false),
                    contact_email = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_titles", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_counter",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_titles",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_counter");

            migrationBuilder.DropTable(
                name: "mtd_cpq_titles");
        }
    }
}
