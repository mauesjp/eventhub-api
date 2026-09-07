using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEventTicketBatchRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TicketBatches_EventId",
                table: "TicketBatches",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketBatches_Events_EventId",
                table: "TicketBatches",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketBatches_Events_EventId",
                table: "TicketBatches");

            migrationBuilder.DropIndex(
                name: "IX_TicketBatches_EventId",
                table: "TicketBatches");
        }
    }
}
