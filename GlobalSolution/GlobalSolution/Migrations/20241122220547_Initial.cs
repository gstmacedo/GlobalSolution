using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalSolution.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    PeriodoDeUso = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PeriodoInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PeriodoFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EnergiaTotal = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    CustoTotal = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LimiteEnergia = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    AlertaEmail = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyConsumptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RelatorioId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EnergiaTotal = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Custo = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyConsumptions_Reports_RelatorioId",
                        column: x => x.RelatorioId,
                        principalTable: "Reports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnergyConsumptions_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UsuarioId",
                table: "Devices",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyConsumptions_RelatorioId",
                table: "EnergyConsumptions",
                column: "RelatorioId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyConsumptions_UsuarioId",
                table: "EnergyConsumptions",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UsuarioId",
                table: "Reports",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UsuarioId",
                table: "UserSettings",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "EnergyConsumptions");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
