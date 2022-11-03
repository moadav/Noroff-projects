using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HvZ_API.Migrations
{
    public partial class Deniz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerCount = table.Column<int>(type: "int", nullable: false),
                    InitZombies = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    HungerDuration = table.Column<double>(type: "float", nullable: false),
                    ChatCooldown = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Is_Started = table.Column<bool>(type: "bit", nullable: false),
                    nw_Lat = table.Column<double>(type: "float", nullable: false),
                    nw_Lng = table.Column<double>(type: "float", nullable: false),
                    se_Lat = table.Column<double>(type: "float", nullable: false),
                    se_Lng = table.Column<double>(type: "float", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameConfigId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_GameConfig_GameConfigId",
                        column: x => x.GameConfigId,
                        principalTable: "GameConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_Human_Visible = table.Column<bool>(type: "bit", nullable: false),
                    Is_Zombie_Visible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mission_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Squad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    MemberCount = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Squad_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHuman = table.Column<bool>(type: "bit", nullable: false),
                    IsPatientZero = table.Column<bool>(type: "bit", nullable: false),
                    HungerTime = table.Column<double>(type: "float", nullable: false),
                    BiteCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMuted = table.Column<bool>(type: "bit", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    SquadId = table.Column<int>(type: "int", nullable: true),
                    VictimId = table.Column<int>(type: "int", nullable: true),
                    CheckinLon = table.Column<double>(type: "float", nullable: true),
                    CheckinLat = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_Squad_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squad",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHumanGlobal = table.Column<bool>(type: "bit", nullable: true),
                    IsZombieGlobal = table.Column<bool>(type: "bit", nullable: true),
                    ChatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    SquadId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chat_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chat_Squad_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squad",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gravestone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfDeath = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    KillerId = table.Column<int>(type: "int", nullable: true),
                    VictimId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gravestone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gravestone_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gravestone_Player_KillerId",
                        column: x => x.KillerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gravestone_Player_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GameConfig",
                columns: new[] { "Id", "ChatCooldown", "Duration", "HungerDuration", "InitZombies", "PlayerCount" },
                values: new object[] { 1, 20.0, 12.0, 1.0, 2, 64 });

            migrationBuilder.InsertData(
                table: "GameConfig",
                columns: new[] { "Id", "ChatCooldown", "Duration", "HungerDuration", "InitZombies", "PlayerCount" },
                values: new object[] { 2, 0.0, 24.0, 2.0, 2, 64 });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "GameConfigId", "Is_Started", "Name", "StartTime", "image", "nw_Lat", "nw_Lng", "se_Lat", "se_Lng" },
                values: new object[] { 1, "First Desc", 1, false, "First Game", new DateTime(2022, 10, 21, 14, 5, 54, 574, DateTimeKind.Local).AddTicks(4338), null, 20.0, 20.0, 20.0, 20.0 });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "GameConfigId", "Is_Started", "Name", "StartTime", "image", "nw_Lat", "nw_Lng", "se_Lat", "se_Lng" },
                values: new object[] { 2, "Second Desc", 2, false, "Second Game", new DateTime(2022, 10, 21, 14, 5, 54, 574, DateTimeKind.Local).AddTicks(4388), null, 0.0, 20.0, 20.0, 20.0 });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "BiteCode", "CheckinLat", "CheckinLon", "FirstName", "GameId", "HungerTime", "IsHuman", "IsMuted", "IsPatientZero", "LastName", "SquadId", "UserId", "VictimId" },
                values: new object[] { 1, "1", null, null, "adea", 1, 10.0, true, false, true, "123", null, "test", null });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "BiteCode", "CheckinLat", "CheckinLon", "FirstName", "GameId", "HungerTime", "IsHuman", "IsMuted", "IsPatientZero", "LastName", "SquadId", "UserId", "VictimId" },
                values: new object[] { 2, "2", null, null, "yesbbb", 1, 10.0, true, false, false, "aaaff", null, "test", null });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "BiteCode", "CheckinLat", "CheckinLon", "FirstName", "GameId", "HungerTime", "IsHuman", "IsMuted", "IsPatientZero", "LastName", "SquadId", "UserId", "VictimId" },
                values: new object[] { 3, "3", null, null, "arnold", 1, 10.0, true, false, true, "tesi", null, "test", null });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_GameId",
                table: "Chat",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_PlayerId",
                table: "Chat",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_SquadId",
                table: "Chat",
                column: "SquadId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameConfigId",
                table: "Game",
                column: "GameConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravestone_GameId",
                table: "Gravestone",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravestone_KillerId",
                table: "Gravestone",
                column: "KillerId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravestone_VictimId",
                table: "Gravestone",
                column: "VictimId",
                unique: true,
                filter: "[VictimId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_GameId",
                table: "Mission",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameId",
                table: "Player",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_SquadId",
                table: "Player",
                column: "SquadId");

            migrationBuilder.CreateIndex(
                name: "IX_Squad_GameId",
                table: "Squad",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Gravestone");

            migrationBuilder.DropTable(
                name: "Mission");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Squad");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "GameConfig");
        }
    }
}
