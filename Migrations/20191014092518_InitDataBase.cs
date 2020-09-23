using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mtd_cpq_catalog",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "varchar(768)", nullable: false),
                    sequence = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    image = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    parent = table.Column<string>(type: "varchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_catalog", x => x.id);
                    table.ForeignKey(
                        name: "fk_cpq_catalog",
                        column: x => x.parent,
                        principalTable: "mtd_cpq_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_group",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "varchar(255)", nullable: false),
                    counter = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_product",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    mtd_cpq_catalog_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "varchar(3027)", nullable: false),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false),
                    som = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    image = table.Column<byte[]>(type: "mediumblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_item_mtd_cpq_catalog1",
                        column: x => x.mtd_cpq_catalog_id,
                        principalTable: "mtd_cpq_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_group_param",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "varchar(512)", nullable: false),
                    prefix = table.Column<string>(type: "varchar(45)", nullable: false),
                    mtd_cpq_config_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    prepared_by = table.Column<string>(type: "varchar(255)", nullable: false),
                    contact_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    contact_phone = table.Column<string>(type: "varchar(255)", nullable: false),
                    contact_email = table.Column<string>(type: "varchar(255)", nullable: false),
                    logo = table.Column<byte[]>(type: "mediumblob", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false),
                    date_creation = table.Column<DateTime>(type: "datetime", nullable: false),
                    title_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    title_note = table.Column<string>(type: "varchar(255)", nullable: false),
                    prepared_for = table.Column<string>(type: "varchar(255)", nullable: false),
                    prepared_by = table.Column<string>(type: "varchar(255)", nullable: false),
                    contact_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    contact_phone = table.Column<string>(type: "varchar(45)", nullable: false),
                    contact_email = table.Column<string>(type: "varchar(45)", nullable: false),
                    description = table.Column<string>(type: "varchar(3072)", nullable: false),
                    logo = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    TimeCr = table.Column<DateTime>(nullable: false),
                    TimeCh = table.Column<DateTime>(nullable: false),
                    total = table.Column<decimal>(type: "decimal(20,2)", nullable: false, defaultValueSql: "'0.00'"),
                    mtd_cpq_group_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    UserCr = table.Column<string>(type: "varchar(36)", nullable: false),
                    UserCh = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_proposal", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_proposal_mtd_cpq_config1",
                        column: x => x.mtd_cpq_group_id,
                        principalTable: "mtd_cpq_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_rule_available",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    product_id_parent = table.Column<string>(type: "varchar(36)", nullable: false),
                    product_id_child = table.Column<string>(type: "varchar(36)", nullable: false),
                    required = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_rule_available", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_rule_available_mtd_cpq_product1",
                        column: x => x.product_id_child,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_available_mtd_cpq_product1",
                        column: x => x.product_id_parent,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "varchar(3027)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    mtd_cpq_proposal_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    mtd_cpq_product_id = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_proposal_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_rule",
                        column: x => x.mtd_cpq_product_id,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_complectation_mtd_cpq_proposal1",
                        column: x => x.mtd_cpq_proposal_id,
                        principalTable: "mtd_cpq_proposal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_catalog",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_number",
                table: "mtd_cpq_catalog",
                column: "id_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_cpq_catalog_idx",
                table: "mtd_cpq_catalog",
                column: "parent");

            migrationBuilder.CreateIndex(
                name: "ix_sequence",
                table: "mtd_cpq_catalog",
                column: "sequence");

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

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_product",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_number_UNIQUE",
                table: "mtd_cpq_product",
                column: "id_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_item_mtd_cpq_catalog1_idx",
                table: "mtd_cpq_product",
                column: "mtd_cpq_catalog_id");

            migrationBuilder.CreateIndex(
                name: "ix_som_index",
                table: "mtd_cpq_product",
                column: "som");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_number",
                table: "mtd_cpq_proposal",
                column: "id_number");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposal_mtd_cpq_config1_idx",
                table: "mtd_cpq_proposal",
                column: "mtd_cpq_group_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal_item",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_rule_idx",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_product_id");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_complectation_mtd_cpq_proposal1_idx",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_proposal_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_rule_available",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_rule_available_mtd_cpq_product1_idx",
                table: "mtd_cpq_rule_available",
                column: "product_id_child");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_available_mtd_cpq_product1_idx",
                table: "mtd_cpq_rule_available",
                column: "product_id_parent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_group_param");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal_item");

            migrationBuilder.DropTable(
                name: "mtd_cpq_rule_available");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal");

            migrationBuilder.DropTable(
                name: "mtd_cpq_product");

            migrationBuilder.DropTable(
                name: "mtd_cpq_group");

            migrationBuilder.DropTable(
                name: "mtd_cpq_catalog");
        }
    }
}
