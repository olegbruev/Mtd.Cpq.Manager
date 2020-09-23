using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProposalCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cpq_catalog",
                table: "mtd_cpq_catalog");

            migrationBuilder.DropForeignKey(
                name: "FK_mtd_cpq_proposal_item_mtd_cpq_product_mtd_cpq_product_id",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropIndex(
                name: "fk_cpq_catalog_idx",
                table: "mtd_cpq_catalog");

            migrationBuilder.DropColumn(
                name: "parent",
                table: "mtd_cpq_catalog");

            migrationBuilder.AddColumn<string>(
                name: "PoposalCatalogId",
                table: "mtd_cpq_proposal_item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentNavigationId",
                table: "mtd_cpq_catalog",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal_catalog",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    mtd_cpq_proposal_id = table.Column<string>(type: "varchar(36)", nullable: true),
                    cid = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "varchar(768)", nullable: true),
                    id_number = table.Column<string>(type: "varchar(45)", nullable: true),
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
                });

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposalitems_catalog_idx",
                table: "mtd_cpq_proposal_item",
                column: "PoposalCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_mtd_cpq_catalog_ParentNavigationId",
                table: "mtd_cpq_catalog",
                column: "ParentNavigationId");

            migrationBuilder.CreateIndex(
                name: "idx_cid",
                table: "mtd_cpq_proposal_catalog",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal_catalog",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposal_catalog_idx",
                table: "mtd_cpq_proposal_catalog",
                column: "mtd_cpq_proposal_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mtd_cpq_catalog_mtd_cpq_catalog_ParentNavigationId",
                table: "mtd_cpq_catalog",
                column: "ParentNavigationId",
                principalTable: "mtd_cpq_catalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposalitems_catalog",
                table: "mtd_cpq_proposal_item",
                column: "PoposalCatalogId",
                principalTable: "mtd_cpq_proposal_catalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mtd_cpq_catalog_mtd_cpq_catalog_ParentNavigationId",
                table: "mtd_cpq_catalog");

            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposalitems_catalog",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal_catalog");

            migrationBuilder.DropIndex(
                name: "fk_mtd_cpq_proposalitems_catalog_idx",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropIndex(
                name: "IX_mtd_cpq_catalog_ParentNavigationId",
                table: "mtd_cpq_catalog");

            migrationBuilder.DropColumn(
                name: "PoposalCatalogId",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "ParentNavigationId",
                table: "mtd_cpq_catalog");

            migrationBuilder.AddColumn<string>(
                name: "parent",
                table: "mtd_cpq_catalog",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "fk_cpq_catalog_idx",
                table: "mtd_cpq_catalog",
                column: "parent");

            migrationBuilder.AddForeignKey(
                name: "fk_cpq_catalog",
                table: "mtd_cpq_catalog",
                column: "parent",
                principalTable: "mtd_cpq_catalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mtd_cpq_proposal_item_mtd_cpq_product_mtd_cpq_product_id",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_product_id",
                principalTable: "mtd_cpq_product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
