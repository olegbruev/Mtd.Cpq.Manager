using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateConfigFiles : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from mtd_cpq_config where id<>''");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "mtd_cpq_notification",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "value_type",
                table: "mtd_cpq_config",
                type: "varchar(45)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mtd_cpq_config_file",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    file_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    file_type = table.Column<string>(type: "varchar(255)", nullable: false),
                    file_data = table.Column<byte[]>(type: "mediumblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_config_file", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_config_file",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_config_file");

            migrationBuilder.DropColumn(
                name: "title",
                table: "mtd_cpq_notification");

            migrationBuilder.DropColumn(
                name: "value_type",
                table: "mtd_cpq_config");
        }
    }
}
