using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(19)", maxLength: 19, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CVC = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CinemaRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    Supports3D = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsAgeRestricted = table.Column<bool>(type: "bit", nullable: false),
                    FilmDuration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieProjections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatsLeft = table.Column<int>(type: "int", nullable: false),
                    StartingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CinemaRoomId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieProjections_CinemaRooms_CinemaRoomId",
                        column: x => x.CinemaRoomId,
                        principalTable: "CinemaRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieProjections_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    ReservationRefundPrice = table.Column<int>(type: "int", nullable: false),
                    MovieProjectionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_MovieProjections_MovieProjectionId",
                        column: x => x.MovieProjectionId,
                        principalTable: "MovieProjections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CinemaRooms",
                columns: new[] { "Id", "AvailableSeats", "Name", "Supports3D" },
                values: new object[,]
                {
                    { 1, 102, "Room1", false },
                    { 2, 126, "Room2", true },
                    { 3, 113, "Room3", true },
                    { 4, 137, "Room4", true },
                    { 5, 127, "Room5", false },
                    { 6, 140, "Room6", false },
                    { 7, 100, "Room7", true },
                    { 8, 109, "Room8", false },
                    { 9, 134, "Room9", false },
                    { 10, 125, "Room10", true },
                    { 11, 117, "Room11", false },
                    { 12, 106, "Room12", true },
                    { 13, 150, "Room13", true },
                    { 14, 132, "Room14", false },
                    { 15, 122, "Room15", true }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Category", "Description", "Director", "FilmDuration", "IsAgeRestricted", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Sci-Fi", "Point watch gun. Popular order since along gun traditional similar. Physical power force cell air concern film. Situation risk politics imagine week.Example change speak worry surface friend executive community.Soon better indeed image quite travel. Senior billion rate official north respond me accept.Lead wide someone top. Audience crime then successful operation toward. Indicate economy include cost product.", "Chris Scott", 172, true, 2, new DateTime(2022, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Endgame" },
                    { 2, "Action", "Protect Congress his issue black social. Up movement tend them. Thing try tell from cultural decade class. Summer there clear away measure class. Most together remember less some once many medical. Song team single among north them benefit. Hundred lawyer between game. Baby war above ok page thing skill. Include feel contain environment continue. Instead strong because actually economic international common. Ground wish hope.Simple song agency seat soldier.", "Thomas Dillon", 70, true, 5, new DateTime(2022, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fight Club" },
                    { 3, "Thriller", "Knowledge meeting stop or wide. Today arm power goal special message small. Walk service media.Anything owner far adult new stand air.Say say happen full several bed head. Sign part adult expect choice. Eight our bill writer interesting structure.Else use listen floor line whole.Around pressure provide run myself. Page evidence send also property.Left tell see grow treat any.", "Deborah Cunningham", 125, true, 1, new DateTime(2015, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inception" },
                    { 4, "Action", "Account dinner thing parent study watch. Indeed condition born last really from. Threat particular right go nice speak hand. Carry mouth clear.Think concern onto wear smile financial.Animal though stay beautiful rich. Prevent either grow born too seven.Project cultural down at need.Another receive marriage drive want culture. Fire able whatever theory such. Staff film condition.Feel suggest worry modern specific allow camera.", "George Mendez", 154, false, 3, new DateTime(2020, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pulp Fiction" },
                    { 5, "Thriller", "Blue effort rich hard audience difference or question. Drive education test rate issue off strategy. Explain exist executive parent ahead. Identify commercial nature order by.Lead ground scientist old middle maybe. Month answer whether perhaps building.Something analysis coach daughter serious notice.Hard nature quite defense adult seat name. Include design top fly fill author million.Plant soon raise class. Manage including dark agreement test note.", "George Lucas", 145, true, 5, new DateTime(2012, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: Episode III - Revenge of the Sith" },
                    { 6, "Sci-Fi", "Large main need later court. City new media help however. Piece success week tell consider.Step light computer charge agree.Degree action space after majority sometimes. Small law tonight choose need.Per break friend general. Energy it attention development in new gas. Science deep affect statement more. Capital minute around tend agreement behind. Eye lead large strong. Type specific value eight either impact. Stand song data painting try require order safe.", "Ashley Brooks", 89, false, 3, new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Godfather" },
                    { 7, "Thriller", "Owner measure every explain. Clearly career until where bill decide forward. Why summer improve summer.New finally fill soldier.News church choose season PM them avoid. Police computer you organization task.May nothing blue sister them score role.Already that hour draw. Our wind whom tax move heavy clearly.Heavy movement color other loss chair live star.From including we middle. Summer sure against well join hear board drop.", "David Hoover", 1, true, 1, new DateTime(2005, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: Return of the King" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieProjections_CinemaRoomId",
                table: "MovieProjections",
                column: "CinemaRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieProjections_MovieId",
                table: "MovieProjections",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MovieProjectionId",
                table: "Tickets",
                column: "MovieProjectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankCards");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "MovieProjections");

            migrationBuilder.DropTable(
                name: "CinemaRooms");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
