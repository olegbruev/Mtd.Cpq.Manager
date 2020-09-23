using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class DropRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposal_product",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_rule",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.RenameColumn(
                name: "master-price",
                table: "mtd_cpq_proposal",
                newName: "master_price");

            migrationBuilder.RenameColumn(
                name: "master_product_id",
                table: "mtd_cpq_proposal",
                newName: "master_id");

            migrationBuilder.RenameIndex(
                name: "IX_mtd_cpq_proposal_master_product_id",
                table: "mtd_cpq_proposal",
                newName: "idx_master_id");

            migrationBuilder.AddColumn<byte[]>(
                name: "master_image",
                table: "mtd_cpq_proposal",
                type: "mediumblob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "master_name",
                table: "mtd_cpq_proposal",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "master_note",
                table: "mtd_cpq_proposal",
                type: "varchar(6048)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "master_number",
                table: "mtd_cpq_proposal",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_mtd_cpq_proposal_item_mtd_cpq_product_mtd_cpq_product_id",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_product_id",
                principalTable: "mtd_cpq_product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mtd_cpq_proposal_item_mtd_cpq_product_mtd_cpq_product_id",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "master_image",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "master_name",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "master_note",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "master_number",
                table: "mtd_cpq_proposal");

            migrationBuilder.RenameColumn(
                name: "master_price",
                table: "mtd_cpq_proposal",
                newName: "master-price");

            migrationBuilder.RenameColumn(
                name: "master_id",
                table: "mtd_cpq_proposal",
                newName: "master_product_id");

            migrationBuilder.RenameIndex(
                name: "idx_master_id",
                table: "mtd_cpq_proposal",
                newName: "IX_mtd_cpq_proposal_master_product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposal_product",
                table: "mtd_cpq_proposal",
                column: "master_product_id",
                principalTable: "mtd_cpq_product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_rule",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_product_id",
                principalTable: "mtd_cpq_product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
