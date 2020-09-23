using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class OneOfList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "one_of",
                table: "mtd_cpq_rule_available",
                newName: "one_of_id");

            migrationBuilder.RenameColumn(
                name: "one_of",
                table: "mtd_cpq_proposal_item",
                newName: "mtd_cpq_proposal_one_of_id");

            migrationBuilder.CreateTable(
                name: "mtd_cpq_one_of",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "varchar(45)", nullable: true),
                    import_tag = table.Column<string>(type: "varchar(45)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_cpq_one_of", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal_one_of",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    cid = table.Column<string>(type: "varchar(36)", nullable: false),
                    mtd_cpq_proposal_id = table.Column<string>(type: "varchar(36)", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "varchar(45)", nullable: true)
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
                });

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_rule_availablel_item_one_of_idx",
                table: "mtd_cpq_rule_available",
                column: "one_of_id");

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposalitems_one_of_idx",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_proposal_one_of_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_one_of",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_cid",
                table: "mtd_cpq_proposal_one_of",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal_one_of",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_mtd_cpq_proposal_one_of_idx",
                table: "mtd_cpq_proposal_one_of",
                column: "mtd_cpq_proposal_id");

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_proposalitems_one_of",
                table: "mtd_cpq_proposal_item",
                column: "mtd_cpq_proposal_one_of_id",
                principalTable: "mtd_cpq_proposal_one_of",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_mtd_cpq_rule_available_item_one_of",
                table: "mtd_cpq_rule_available",
                column: "one_of_id",
                principalTable: "mtd_cpq_one_of",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_proposalitems_one_of",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropForeignKey(
                name: "fk_mtd_cpq_rule_available_item_one_of",
                table: "mtd_cpq_rule_available");

            migrationBuilder.DropTable(
                name: "mtd_cpq_one_of");

            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal_one_of");

            migrationBuilder.DropIndex(
                name: "fk_mtd_cpq_rule_availablel_item_one_of_idx",
                table: "mtd_cpq_rule_available");

            migrationBuilder.DropIndex(
                name: "fk_mtd_cpq_proposalitems_one_of_idx",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.RenameColumn(
                name: "one_of_id",
                table: "mtd_cpq_rule_available",
                newName: "one_of");

            migrationBuilder.RenameColumn(
                name: "mtd_cpq_proposal_one_of_id",
                table: "mtd_cpq_proposal_item",
                newName: "one_of");
        }
    }
}
