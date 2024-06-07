using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OceanTech.Migrations
{
    /// <inheritdoc />
    public partial class DBInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_GS_Usuario",
                columns: table => new
                {
                    us_int_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    us_string_nome = table.Column<string>(type: "NVARCHAR2(225)", maxLength: 225, nullable: false),
                    us_string_email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    us_string_senha = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    us_dat_inscricao = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    ativo = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GS_Usuario", x => x.us_int_id);
                });

            migrationBuilder.CreateTable(
                name: "TB_GS_GameDiario",
                columns: table => new
                {
                    gd_int_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    gd_int_Jogou = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    gd_dat_DataJogou = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false),
                    us_int_id = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GS_GameDiario", x => x.gd_int_id);
                    table.ForeignKey(
                        name: "FK_GS_GameDiarioUsuario",
                        column: x => x.us_int_id,
                        principalTable: "TB_GS_Usuario",
                        principalColumn: "us_int_id");
                });

            migrationBuilder.CreateTable(
                name: "TB_GS_Pontuacao",
                columns: table => new
                {
                    pc_int_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    pc_int_pontuacaoMensal = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    us_int_id = table.Column<decimal>(type: "NUMBER(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GS_Pontuacao", x => x.pc_int_id);
                    table.ForeignKey(
                        name: "FK_GS_PontuacaoUsuario",
                        column: x => x.us_int_id,
                        principalTable: "TB_GS_Usuario",
                        principalColumn: "us_int_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_GS_PontuacaoDiarias",
                columns: table => new
                {
                    pd_int_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    pd_int_valor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    gd_int_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GS_PontuacaoDiarias", x => x.pd_int_id);
                    table.ForeignKey(
                        name: "FK_GS_PontuacaoDiariaGameDiario",
                        column: x => x.gd_int_id,
                        principalTable: "TB_GS_GameDiario",
                        principalColumn: "gd_int_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_GS_GameDiario_us_int_id",
                table: "TB_GS_GameDiario",
                column: "us_int_id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_GS_Pontuacao_us_int_id",
                table: "TB_GS_Pontuacao",
                column: "us_int_id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_GS_PontuacaoDiarias_gd_int_id",
                table: "TB_GS_PontuacaoDiarias",
                column: "gd_int_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_GS_Pontuacao");

            migrationBuilder.DropTable(
                name: "TB_GS_PontuacaoDiarias");

            migrationBuilder.DropTable(
                name: "TB_GS_GameDiario");

            migrationBuilder.DropTable(
                name: "TB_GS_Usuario");
        }
    }
}
