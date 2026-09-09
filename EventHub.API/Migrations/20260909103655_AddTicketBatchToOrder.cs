using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketBatchToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketBatchId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TicketBatchId",
                table: "Orders",
                column: "TicketBatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TicketBatches_TicketBatchId",
                table: "Orders",
                column: "TicketBatchId",
                principalTable: "TicketBatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TicketBatches_TicketBatchId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TicketBatchId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TicketBatchId",
                table: "Orders");
        }
    }
}
