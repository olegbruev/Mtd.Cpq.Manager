using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateChangeNoties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "mtd_cpq_reader_user");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "mtd_cpq_notification");

            migrationBuilder.RenameIndex(
                name: "IX_mtd_cpq_reader_user_message_id",
                table: "mtd_cpq_reader_user",
                newName: "fk_notification_user_idx");

            migrationBuilder.AddColumn<string>(
                name: "user_name",
                table: "mtd_cpq_reader_user",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "mtd_cpq_notification",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AddColumn<string>(
                name: "user_name",
                table: "mtd_cpq_notification",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "idx-username",
                table: "mtd_cpq_reader_user",
                column: "user_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx-username",
                table: "mtd_cpq_reader_user");

            migrationBuilder.DropColumn(
                name: "user_name",
                table: "mtd_cpq_reader_user");

            migrationBuilder.DropColumn(
                name: "user_name",
                table: "mtd_cpq_notification");

            migrationBuilder.RenameIndex(
                name: "fk_notification_user_idx",
                table: "mtd_cpq_reader_user",
                newName: "IX_mtd_cpq_reader_user_message_id");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "mtd_cpq_reader_user",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "mtd_cpq_notification",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "mtd_cpq_notification",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");
        }
    }
}
