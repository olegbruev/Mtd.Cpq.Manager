using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class ProposalItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "id_number",
                table: "mtd_cpq_proposal_item",
                type: "varchar(45)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "mtd_cpq_proposal_item",
                type: "mediumblob",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_number",
                table: "mtd_cpq_proposal_item");

            migrationBuilder.DropColumn(
                name: "image",
                table: "mtd_cpq_proposal_item");
        }
    }
}
