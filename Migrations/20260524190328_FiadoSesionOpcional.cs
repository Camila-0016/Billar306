using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billar306.API.Migrations
{
    /// <inheritdoc />
    public partial class FiadoSesionOpcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SesionMesaId",
                table: "Fiados",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$TGPI6zPcspn7cu.ufzKSi./GBWL.IRKq8WoCgU8PG8yT7wJsIN5sC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SesionMesaId",
                table: "Fiados",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$DzqxW/SL5yYY7PKck3bTLOvdxo94xO/oDcmCwPbggaA5NBYauT6KO");
        }
    }
}
