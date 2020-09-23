using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "mtd_cpq_titles",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mtd_cpq_config",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    title_logo = table.Column<byte[]>(type: "mediumblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_config", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_config",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_config");

            migrationBuilder.DropColumn(
                name: "language",
                table: "mtd_cpq_titles");
        }
    }
}
