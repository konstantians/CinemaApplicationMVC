using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModelUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_MovieProjections_MovieProjectionId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameColumn(
                name: "CinemaRoomId",
                table: "CinemaRooms",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_MovieProjectionId",
                table: "Reservations",
                newName: "IX_Reservations_MovieProjectionId");

            migrationBuilder.AddColumn<int>(
                name: "ReservationRefoundPrice",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_MovieProjections_MovieProjectionId",
                table: "Reservations",
                column: "MovieProjectionId",
                principalTable: "MovieProjections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_MovieProjections_MovieProjectionId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationRefoundPrice",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CinemaRooms",
                newName: "CinemaRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_MovieProjectionId",
                table: "Reservation",
                newName: "IX_Reservation_MovieProjectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_MovieProjections_MovieProjectionId",
                table: "Reservation",
                column: "MovieProjectionId",
                principalTable: "MovieProjections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
