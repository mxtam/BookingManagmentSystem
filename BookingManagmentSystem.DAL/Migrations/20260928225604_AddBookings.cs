using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagmentSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RentalCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ServicesCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.CheckConstraint("CK_Bookings_HourlyRate", "[HourlyRate] >= 0");
                    table.CheckConstraint("CK_Bookings_RentalCost", "[RentalCost] >= 0");
                    table.CheckConstraint("CK_Bookings_ServicesCost", "[ServicesCost] >= 0");
                    table.CheckConstraint("CK_Bookings_TimeRange", "[EndTime] > [StartTime]");
                    table.ForeignKey(
                        name: "FK_Bookings_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingServices",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingServices", x => new { x.BookingId, x.ServiceId });
                    table.CheckConstraint("CK_BookingServices_Price", "[Price] >= 0");
                    table.ForeignKey(
                        name: "FK_BookingServices_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HallId_StartTime_EndTime",
                table: "Bookings",
                columns: new[] { "HallId", "StartTime", "EndTime" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingServices_ServiceId",
                table: "BookingServices",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingServices");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
