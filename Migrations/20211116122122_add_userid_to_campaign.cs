using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_Guide.Migrations
{
    public partial class add_userid_to_campaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Campaigns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_UserId",
                table: "Campaigns",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_AspNetUsers_UserId",
                table: "Campaigns",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_AspNetUsers_UserId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_UserId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Campaigns");
        }
    }
}
