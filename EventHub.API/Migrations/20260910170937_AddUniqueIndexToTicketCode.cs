using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToTicketCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Code",
                table: "Tickets",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_Code",
                table: "Tickets");
        }
    }
}
