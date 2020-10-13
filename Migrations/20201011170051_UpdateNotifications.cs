using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title_logo",
                table: "mtd_cpq_config");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "mtd_cpq_config",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "mtd_cpq_config",
                type: "longtext",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mtd_cpq_notification",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    message = table.Column<string>(type: "longtext", nullable: false),
                    user_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    timecr = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_notification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_reader_user",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    message_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    user_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    timecr = table.Column<string>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_reader_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_notification_user",
                        column: x => x.message_id,
                        principalTable: "mtd_cpq_notification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_notification",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_reader_user",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mtd_cpq_reader_user_message_id",
                table: "mtd_cpq_reader_user",
                column: "message_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_reader_user");

            migrationBuilder.DropTable(
                name: "mtd_cpq_notification");

            migrationBuilder.DropColumn(
                name: "name",
                table: "mtd_cpq_config");

            migrationBuilder.DropColumn(
                name: "value",
                table: "mtd_cpq_config");

            migrationBuilder.AddColumn<byte[]>(
                name: "title_logo",
                table: "mtd_cpq_config",
                type: "mediumblob",
                nullable: true);
        }
    }
}
