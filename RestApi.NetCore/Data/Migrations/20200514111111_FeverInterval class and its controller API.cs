using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApi.NetCore.Data.Migrations
{
    public partial class FeverIntervalclassanditscontrollerAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyTemperature_AspNetUsers_UserId",
                table: "BodyTemperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyTemperature",
                table: "BodyTemperature");

            migrationBuilder.RenameTable(
                name: "BodyTemperature",
                newName: "BodyTemperatures");

            migrationBuilder.RenameIndex(
                name: "IX_BodyTemperature_UserId",
                table: "BodyTemperatures",
                newName: "IX_BodyTemperatures_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyTemperatures",
                table: "BodyTemperatures",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FeverIntervals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartedTemperature = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeverIntervals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeverIntervals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeverIntervals_UserId",
                table: "FeverIntervals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyTemperatures_AspNetUsers_UserId",
                table: "BodyTemperatures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyTemperatures_AspNetUsers_UserId",
                table: "BodyTemperatures");

            migrationBuilder.DropTable(
                name: "FeverIntervals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyTemperatures",
                table: "BodyTemperatures");

            migrationBuilder.RenameTable(
                name: "BodyTemperatures",
                newName: "BodyTemperature");

            migrationBuilder.RenameIndex(
                name: "IX_BodyTemperatures_UserId",
                table: "BodyTemperature",
                newName: "IX_BodyTemperature_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyTemperature",
                table: "BodyTemperature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyTemperature_AspNetUsers_UserId",
                table: "BodyTemperature",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
