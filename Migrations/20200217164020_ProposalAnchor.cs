using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProposalAnchor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mtd_cpq_proposal_anchor",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    cid = table.Column<string>(type: "varchar(36)", nullable: false),
                    mtd_cpq_product_id = table.Column<string>(type: "varchar(36)", nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "fk_proposal_anchor_idx",
                table: "mtd_cpq_proposal_anchor",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_cpq_proposal_anchor",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mtd_cpq_proposal_anchor");
        }
    }
}
