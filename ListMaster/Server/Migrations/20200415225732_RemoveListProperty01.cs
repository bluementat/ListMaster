using Microsoft.EntityFrameworkCore.Migrations;

namespace ListMaster.Server.Migrations
{
    public partial class RemoveListProperty01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterLists_AspNetUsers_CreatorId",
                table: "MasterLists");

            migrationBuilder.DropIndex(
                name: "IX_MasterLists_CreatorId",
                table: "MasterLists");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "MasterLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "MasterLists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterLists_CreatorId",
                table: "MasterLists",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterLists_AspNetUsers_CreatorId",
                table: "MasterLists",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
