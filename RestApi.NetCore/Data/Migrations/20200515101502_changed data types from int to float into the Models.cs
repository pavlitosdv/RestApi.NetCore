using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApi.NetCore.Data.Migrations
{
    public partial class changeddatatypesfrominttofloatintotheModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "StartedTemperature",
                table: "FeverIntervals",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature",
                table: "BodyTemperatures",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StartedTemperature",
                table: "FeverIntervals",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Temperature",
                table: "BodyTemperatures",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
