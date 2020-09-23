using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateRemoveGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_group_param",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropTable(
                name: "mtd_cpq_group_param");

            migrationBuilder.DropTable(
                name: "mtd_cpq_group");

            migrationBuilder.DropIndex(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1_idx",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropIndex(
                name: "fk_mtd_cpq_proposal_group_param_idx",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "mtd_cpq_group_id",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "mtd_cpq_group_param_id",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "TimeCh",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "TimeCr",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "UserCh",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "UserCr",
                table: "mtd_cpq_proposal");

            migrationBuilder.AlterColumn<long>(
                name: "id_number",
                table: "mtd_cpq_counter",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mtd_cpq_group_id",
                table: "mtd_cpq_proposal",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mtd_cpq_group_param_id",
                table: "mtd_cpq_proposal",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCh",
                table: "mtd_cpq_proposal",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCr",
                table: "mtd_cpq_proposal",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserCh",
                table: "mtd_cpq_proposal",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCr",
                table: "mtd_cpq_proposal",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "id_number",
                table: "mtd_cpq_counter",
                type: "decimal",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_group",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    change_rule_relations = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    counter = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "varchar(255)", nullable: true),
                    print_gross_price = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    title_image = table.Column<byte[]>(type: "mediumblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_group_param",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    contact_email = table.Column<string>(type: "varchar(255)", nullable: true),
                    contact_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    contact_phone = table.Column<string>(type: "varchar(255)", nullable: false),
                    language = table.Column<string>(type: "varchar(45)", nullable: false, defaultValueSql: "'en-US'"),
                    logo = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    mtd_cpq_config_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "varchar(512)", nullable: false),
                    prefix = table.Column<string>(type: "varchar(45)", nullable: false),
                    prepared_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_group_param", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_config_param_mtd_cpq_config1",
                        column: x => x.mtd_cpq_config_id,
                        principalTable: "mtd_cpq_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1_idx",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_id");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposal_group_param_idx",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_param_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_group",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "name_UNIQUE",
                table: "mtd_cpq_group",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_group_param",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_config_param_mtd_cpq_config1_idx",
                table: "mtd_cpq_group_param",
                column: "mtd_cpq_config_id");

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_id",
                principalTable: "mtd_cpq_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_group_param",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_param_id",
                principalTable: "mtd_cpq_group_param",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
