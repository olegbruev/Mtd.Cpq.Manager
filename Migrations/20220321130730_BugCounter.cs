using Microsoft.EntityFrameworkCore.Migrations;
using Mtd.Cpq.Manager.DataConfig;

namespace Mtd.Cpq.Manager.Migrations
{
    public partial class BugCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"insert into mtd_cpq_counter (id,id_number) values('{Constant.GuidProposalCounter}',0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"delete from mtd_cpq_counter where id<>''");
        }
    }
}
