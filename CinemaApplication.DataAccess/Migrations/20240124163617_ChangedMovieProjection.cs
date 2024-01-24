using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedMovieProjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AirDate",
                table: "MovieProjections",
                newName: "StartingTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndingTime",
                table: "MovieProjections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndingTime",
                table: "MovieProjections");

            migrationBuilder.RenameColumn(
                name: "StartingTime",
                table: "MovieProjections",
                newName: "AirDate");
        }
    }
}
