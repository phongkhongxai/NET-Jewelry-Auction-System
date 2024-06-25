using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuctionRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionRequests_Auctions_AuctionId",
                table: "AuctionRequests");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "AuctionRequests",
                newName: "JewerlryId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionRequests_AuctionId",
                table: "AuctionRequests",
                newName: "IX_AuctionRequests_JewerlryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionRequests_Jewelries_JewerlryId",
                table: "AuctionRequests",
                column: "JewerlryId",
                principalTable: "Jewelries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionRequests_Jewelries_JewerlryId",
                table: "AuctionRequests");

            migrationBuilder.RenameColumn(
                name: "JewerlryId",
                table: "AuctionRequests",
                newName: "AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionRequests_JewerlryId",
                table: "AuctionRequests",
                newName: "IX_AuctionRequests_AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionRequests_Auctions_AuctionId",
                table: "AuctionRequests",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
