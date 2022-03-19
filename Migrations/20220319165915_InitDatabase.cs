using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_catalog",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    note = table.Column<string>(type: "varchar(768)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sequence = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    image = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    import_tag = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_catalog", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_config",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value_type = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_config", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_config_file",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    file_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    file_type = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    file_data = table.Column<byte[]>(type: "mediumblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_config_file", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_counter",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_number = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_counter", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_import",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    time_cr = table.Column<DateTime>(type: "datetime", nullable: false),
                    user_id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status_text = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status_process = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    note_load = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    old_to_archive = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    datasheet_load = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_import", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_import_param",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    col_number = table.Column<int>(type: "int(11)", nullable: false),
                    col_name = table.Column<int>(type: "int(11)", nullable: false),
                    col_note = table.Column<int>(type: "int(11)", nullable: false),
                    col_data = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    col_price = table.Column<int>(type: "int(11)", nullable: false),
                    tag_data = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tag_master = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tag_required = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_import_param", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_notification",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    timecr = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_notification", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_one_of",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    note = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    import_tag = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_one_of", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_creation = table.Column<DateTime>(type: "datetime", nullable: false),
                    title_name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title_note = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prepared_for = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prepared_by = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_name = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_phone = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_email = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(3072)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logo = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    logo_width = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    logo_height = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    logo_flexible = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    price_customer = table.Column<decimal>(type: "decimal(20,2)", nullable: false, defaultValueSql: "'0.00'"),
                    customer_currency = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    config_master_included = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    config_change_rule = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    delivery_condition = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    master_id = table.Column<string>(type: "varchar(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    master_number = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    master_name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    master_note = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    master_datasheet = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    master_image = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    master_price = table.Column<decimal>(type: "decimal(20,2)", nullable: false, defaultValueSql: "'0.00'"),
                    master_qty = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    master_afactor = table.Column<decimal>(type: "decimal(20,2)", nullable: false, defaultValueSql: "'1'"),
                    view_note = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    view_price_gross = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    view_price_customer = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    view_delivery = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    view_qty = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    view_images = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    view_afactor = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    view_proposal = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    view_datasheet = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    language = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_proposal", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_titles",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logo = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    logo_width = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    logo_height = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    logo_flexible = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prepared_by = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_phone = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_email = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    language = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_titles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_product",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_catalog_id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    note = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datasheet = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    som = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    trial = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    image = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    price = table.Column<decimal>(type: "decimal(20,2)", nullable: false, defaultValueSql: "'0'"),
                    archive = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    sequence = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    afactor = table.Column<decimal>(type: "decimal(20,2)", nullable: false, defaultValueSql: "'1'")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_import_data",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_import_id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    note = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datasheet = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(20,2)", nullable: false, defaultValueSql: "'0.00'"),
                    required = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    parent = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    catalog_tag = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    one_of = table.Column<string>(type: "varchar(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    master_product = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_reader_user",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message_id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    timecr = table.Column<DateTime>(type: "datetime", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal_catalog",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_proposal_id = table.Column<string>(type: "varchar(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cid = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    note = table.Column<string>(type: "varchar(768)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    sequence = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_proposal_catalog", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_proposal_catalog",
                        column: x => x.mtd_cpq_proposal_id,
                        principalTable: "mtd_cpq_proposal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal_one_of",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cid = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_proposal_id = table.Column<string>(type: "varchar(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    note = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_proposal_one_of", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_proposal_one_of",
                        column: x => x.mtd_cpq_proposal_id,
                        principalTable: "mtd_cpq_proposal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_rule_anchor",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_master = table.Column<string>(type: "varchar(36)", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_anchor = table.Column<string>(type: "varchar(36)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    notice = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_rule_anchor", x => x.id);
                    table.ForeignKey(
                        name: "fk_anchor_product_anchor",
                        column: x => x.product_anchor,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_anchor_product_master",
                        column: x => x.product_master,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_rule_available",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_id_parent = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_id_child = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    required = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    one_of_id = table.Column<string>(type: "varchar(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_rule_available", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_available_mtd_cpq_product1",
                        column: x => x.product_id_parent,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_rule_available_item_one_of",
                        column: x => x.one_of_id,
                        principalTable: "mtd_cpq_one_of",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_rule_available_mtd_cpq_product1",
                        column: x => x.product_id_child,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    note = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datasheet = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<byte[]>(type: "mediumblob", nullable: true),
                    selected = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    required = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    mtd_cpq_proposal_id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_product_id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_proposal_catalog_id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    qty = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'1'"),
                    mtd_cpq_proposal_one_of_id = table.Column<string>(type: "varchar(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    anchor_notice = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    anchor_history = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    sequence = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'0'"),
                    forbidden = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_proposal_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_complectation_mtd_cpq_proposal1",
                        column: x => x.mtd_cpq_proposal_id,
                        principalTable: "mtd_cpq_proposal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_proposalitems_catalog",
                        column: x => x.mtd_cpq_proposal_catalog_id,
                        principalTable: "mtd_cpq_proposal_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtd_cpq_proposalitems_one_of",
                        column: x => x.mtd_cpq_proposal_one_of_id,
                        principalTable: "mtd_cpq_proposal_one_of",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_rule_anchor_bind",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_rule_anchor_id = table.Column<string>(type: "varchar(36)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_product_id = table.Column<string>(type: "varchar(36)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    include = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    required = table.Column<sbyte>(type: "tinyint(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_rule_anchor_bind", x => x.id);
                    table.ForeignKey(
                        name: "fk_anchor_required_anchor",
                        column: x => x.mtd_cpq_rule_anchor_id,
                        principalTable: "mtd_cpq_rule_anchor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_anchor_required_product",
                        column: x => x.mtd_cpq_product_id,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal_anchor",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cid = table.Column<string>(type: "varchar(36)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mtd_cpq_product_id = table.Column<string>(type: "varchar(36)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    include = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    required = table.Column<sbyte>(type: "tinyint(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_proposal_anchor", x => x.id);
                    table.ForeignKey(
                        name: "fk_proposal_anchor",
                        column: x => x.cid,
                        principalTable: "mtd_cpq_proposal_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_catalog",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_number",
                table: "mtd_cpq_catalog",
                column: "id_number");

            migrationBuilder.CreateIndex(
                name: "ix_sequence",
                table: "mtd_cpq_catalog",
                column: "sequence");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_config",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_config_file",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_counter",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_import",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_process",
                table: "mtd_cpq_import",
                column: "status_process");

            migrationBuilder.CreateIndex(
                name: "fk_import_data_history_idx",
                table: "mtd_cpq_import_data",
                column: "mtd_cpq_import_id");

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
                name: "id_UNIQUE",
                table: "mtd_cpq_import_param",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_notification",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_one_of",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_item_mtd_cpq_catalog1_idx",
                table: "mtd_cpq_product",
                column: "mtd_cpq_catalog_id");

            migrationBuilder.CreateIndex(
                name: "id_number_UNIQUE",
                table: "mtd_cpq_product",
                column: "id_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_product",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_archive",
                table: "mtd_cpq_product",
                column: "archive");

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
                name: "idx_master_id",
                table: "mtd_cpq_proposal",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "ix_datecreation",
                table: "mtd_cpq_proposal",
                column: "date_creation");

            migrationBuilder.CreateIndex(
                name: "ix_number",
                table: "mtd_cpq_proposal",
                column: "id_number");

            migrationBuilder.CreateIndex(
                name: "fk_proposal_anchor_idx",
                table: "mtd_cpq_proposal_anchor",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal_anchor",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposal_catalog_idx",
                table: "mtd_cpq_proposal_catalog",
                column: "mtd_cpq_proposal_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal_catalog",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_cid",
                table: "mtd_cpq_proposal_catalog",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_complectation_mtd_cpq_proposal1_idx",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_proposal_id");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposalitems_catalog_idx",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_proposal_catalog_id");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposalitems_one_of_idx",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_proposal_one_of_id");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_rule_idx",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_product_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal_item",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REQUIRED",
                table: "mtd_cpq_proposal_item",
                column: "required");

            migrationBuilder.CreateIndex(
                name: "IX_SELECTED",
                table: "mtd_cpq_proposal_item",
                column: "selected");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposal_one_of_idx",
                table: "mtd_cpq_proposal_one_of",
                column: "mtd_cpq_proposal_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal_one_of",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_cid",
                table: "mtd_cpq_proposal_one_of",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "fk_notification_user_idx",
                table: "mtd_cpq_reader_user",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_reader_user",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx-username",
                table: "mtd_cpq_reader_user",
                column: "user_name");

            migrationBuilder.CreateIndex(
                name: "fk_anchor_product_anchor_idx",
                table: "mtd_cpq_rule_anchor",
                column: "product_anchor");

            migrationBuilder.CreateIndex(
                name: "fk_anchor_product_master_idx",
                table: "mtd_cpq_rule_anchor",
                column: "product_master");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_rule_anchor",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_anchor_required_anchor_idx",
                table: "mtd_cpq_rule_anchor_bind",
                column: "mtd_cpq_rule_anchor_id");

            migrationBuilder.CreateIndex(
                name: "fk_anchor_required_idx",
                table: "mtd_cpq_rule_anchor_bind",
                column: "mtd_cpq_product_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_rule_anchor_bind",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_available_mtd_cpq_product1_idx",
                table: "mtd_cpq_rule_available",
                column: "product_id_parent");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_rule_available_mtd_cpq_product1_idx",
                table: "mtd_cpq_rule_available",
                column: "product_id_child");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_rule_availablel_item_one_of_idx",
                table: "mtd_cpq_rule_available",
                column: "one_of_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_rule_available",
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
                name: "mtd_cpq_config");

            migrationBuilder.DropTable(
                name: "mtd_cpq_config_file");

            migrationBuilder.DropTable(
                name: "mtd_cpq_counter");

            migrationBuilder.DropTable(
                name: "mtd_cpq_import_data");

            migrationBuilder.DropTable(
                name: "mtd_cpq_import_param");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal_anchor");

            migrationBuilder.DropTable(
                name: "mtd_cpq_reader_user");

            migrationBuilder.DropTable(
                name: "mtd_cpq_rule_anchor_bind");

            migrationBuilder.DropTable(
                name: "mtd_cpq_rule_available");

            migrationBuilder.DropTable(
                name: "mtd_cpq_titles");

            migrationBuilder.DropTable(
                name: "mtd_cpq_import");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal_item");

            migrationBuilder.DropTable(
                name: "mtd_cpq_notification");

            migrationBuilder.DropTable(
                name: "mtd_cpq_rule_anchor");

            migrationBuilder.DropTable(
                name: "mtd_cpq_one_of");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal_catalog");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal_one_of");

            migrationBuilder.DropTable(
                name: "mtd_cpq_product");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal");

            migrationBuilder.DropTable(
                name: "mtd_cpq_catalog");
        }
    }
}
