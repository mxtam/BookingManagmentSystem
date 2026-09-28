using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagmentSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintsAndIndexesForHallAndService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Services_Title",
                table: "Services",
                column: "Title",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Services_Price",
                table: "Services",
                sql: "[Price] >= 0 AND [Price] <= 10000");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_Title",
                table: "Halls",
                column: "Title",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Halls_Capacity",
                table: "Halls",
                sql: "[Capacity] >= 1 AND [Capacity] <= 500");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Halls_Price",
                table: "Halls",
                sql: "[Price] >= 0 AND [Price] <= 10000");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Services_Title",
                table: "Services");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Services_Price",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Halls_Title",
                table: "Halls");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Halls_Capacity",
                table: "Halls");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Halls_Price",
                table: "Halls");
        }
    }
}
