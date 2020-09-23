using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class AddAnchor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mtd_cpq_rule_anchor",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    product_master = table.Column<string>(type: "varchar(36)", nullable: true),
                    product_anchor = table.Column<string>(type: "varchar(36)", nullable: false),
                    notice = table.Column<string>(type: "text", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_rule_anchor_bind",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    mtd_cpq_rule_anchor_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    mtd_cpq_product_id = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_rule_anchor_bind", x => x.id);
                    table.ForeignKey(
                        name: "fk_anchor_required_product",
                        column: x => x.mtd_cpq_product_id,
                        principalTable: "mtd_cpq_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_anchor_required_anchor",
                        column: x => x.mtd_cpq_rule_anchor_id,
                        principalTable: "mtd_cpq_rule_anchor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_rule_anchor",
                column: "id",
                unique: true);

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
                table: "mtd_cpq_rule_anchor_bind",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_anchor_required_idx",
                table: "mtd_cpq_rule_anchor_bind",
                column: "mtd_cpq_product_id");

            migrationBuilder.CreateIndex(
                name: "fk_anchor_required_anchor_idx",
                table: "mtd_cpq_rule_anchor_bind",
                column: "mtd_cpq_rule_anchor_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_rule_anchor_bind");

            migrationBuilder.DropTable(
                name: "mtd_cpq_rule_anchor");
        }
    }
}
