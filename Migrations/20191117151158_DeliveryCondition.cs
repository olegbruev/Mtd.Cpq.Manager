using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class DeliveryCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposalitems_catalog",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropIndex(
                name: "fk_mtd_cpq_proposalitems_catalog_idx",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "mtd_cpq_proposal",
                newName: "price_gross");

            migrationBuilder.AddColumn<string>(
                name: "mtd_cpq_proposal_catalog_id",
                table: "mtd_cpq_proposal_item",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "qty",
                table: "mtd_cpq_proposal_item",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'1'");

            migrationBuilder.AddColumn<string>(
                name: "customer_currency",
                table: "mtd_cpq_proposal",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "delivery_condition",
                table: "mtd_cpq_proposal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "master_qty",
                table: "mtd_cpq_proposal",
                type: "int",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<decimal>(
                name: "price_customer",
                table: "mtd_cpq_proposal",
                type: "decimal(20,2)",
                nullable: false,
                defaultValueSql: "'0.00'");

            migrationBuilder.AddColumn<int>(
                name: "type_config",
                table: "mtd_cpq_proposal",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'1'");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposalitems_catalog_idx",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_proposal_catalog_id");

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposalitems_catalog",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_proposal_catalog_id",
                principalTable: "mtd_cpq_proposal_catalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("update mtd_cpq_proposal_item set mtd_cpq_proposal_catalog_id=PoposalCatalogId");

            migrationBuilder.DropColumn(
                name: "PoposalCatalogId",
                table: "mtd_cpq_proposal_item");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposalitems_catalog",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropIndex(
                name: "fk_mtd_cpq_proposalitems_catalog_idx",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "mtd_cpq_proposal_catalog_id",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "qty",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "customer_currency",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "delivery_condition",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "master_qty",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "price_customer",
                table: "mtd_cpq_proposal");

            migrationBuilder.DropColumn(
                name: "type_config",
                table: "mtd_cpq_proposal");

            migrationBuilder.RenameColumn(
                name: "price_gross",
                table: "mtd_cpq_proposal",
                newName: "total");

            migrationBuilder.AddColumn<string>(
                name: "PoposalCatalogId",
                table: "mtd_cpq_proposal_item",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposalitems_catalog_idx",
                table: "mtd_cpq_proposal_item",
                column: "PoposalCatalogId");

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposalitems_catalog",
                table: "mtd_cpq_proposal_item",
                column: "PoposalCatalogId",
                principalTable: "mtd_cpq_proposal_catalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
