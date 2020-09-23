using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class UpdateIndexQuoteDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_datecreation",
                table: "mtd_cpq_proposal",
                column: "date_creation");

            migrationBuilder.Sql("insert into mtd_cpq_counter (id,id_number) values('AC370E24-DBB2-4D9C-A1DF-C902F14F4C40',0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_datecreation",
                table: "mtd_cpq_proposal");
        }
    }
}
