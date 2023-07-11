using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FleetControlWebAPI.Migrations
{
    public partial class InsertDadosTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_motr_veicl",
                keyColumns: new[] { "veicl_id", "motor_id" },
                keyValues: new object[] { "0ba3f5d5-c6c4-485c-aa95-b0dd3e0625e3", "24ee7c1d-8969-4094-8ff4-92d4c3a30c1a" });

            migrationBuilder.DeleteData(
                table: "tbl_motr_veicl",
                keyColumns: new[] { "veicl_id", "motor_id" },
                keyValues: new object[] { "fdf2bbd9-7cdf-4d71-98d2-94a32c9dd100", "dd761061-f588-4cbc-a06e-d88f7559c4f0" });

            migrationBuilder.DeleteData(
                table: "tbl_motor",
                keyColumn: "motor_id",
                keyValue: "24ee7c1d-8969-4094-8ff4-92d4c3a30c1a");

            migrationBuilder.DeleteData(
                table: "tbl_motor",
                keyColumn: "motor_id",
                keyValue: "dd761061-f588-4cbc-a06e-d88f7559c4f0");

            migrationBuilder.DeleteData(
                table: "tbl_veicl",
                keyColumn: "veicl_id",
                keyValue: "0ba3f5d5-c6c4-485c-aa95-b0dd3e0625e3");

            migrationBuilder.DeleteData(
                table: "tbl_veicl",
                keyColumn: "veicl_id",
                keyValue: "fdf2bbd9-7cdf-4d71-98d2-94a32c9dd100");

            migrationBuilder.AlterColumn<string>(
                name: "cnh",
                table: "tbl_motor",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.InsertData(
                table: "tbl_motor",
                columns: new[] { "motor_id", "ativo", "cnh", "nome", "validadeCNH" },
                values: new object[,]
                {
                    { "dc1398ac-bf6b-4cd6-9d9b-b1a50c24ad65", true, "1234567891", "Samuel Almeida", new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "05df018c-4e86-4327-8f3a-6f72c8c15bc9", true, "9876549283", "João da Silva", new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "tbl_veicl",
                columns: new[] { "veicl_id", "ano", "modelo", "placa" },
                values: new object[,]
                {
                    { "8d93e52d-36df-4c6b-9505-239a9ac54966", 2019, "Volvo FH 540", "ABC1234" },
                    { "bfd9f2db-032a-4dbe-8a07-019aab9f29ae", 2018, "Scania R450", "CBA4321" }
                });

            migrationBuilder.InsertData(
                table: "tbl_motr_veicl",
                columns: new[] { "veicl_id", "motor_id" },
                values: new object[] { "8d93e52d-36df-4c6b-9505-239a9ac54966", "dc1398ac-bf6b-4cd6-9d9b-b1a50c24ad65" });

            migrationBuilder.InsertData(
                table: "tbl_motr_veicl",
                columns: new[] { "veicl_id", "motor_id" },
                values: new object[] { "bfd9f2db-032a-4dbe-8a07-019aab9f29ae", "05df018c-4e86-4327-8f3a-6f72c8c15bc9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_motr_veicl",
                keyColumns: new[] { "veicl_id", "motor_id" },
                keyValues: new object[] { "8d93e52d-36df-4c6b-9505-239a9ac54966", "dc1398ac-bf6b-4cd6-9d9b-b1a50c24ad65" });

            migrationBuilder.DeleteData(
                table: "tbl_motr_veicl",
                keyColumns: new[] { "veicl_id", "motor_id" },
                keyValues: new object[] { "bfd9f2db-032a-4dbe-8a07-019aab9f29ae", "05df018c-4e86-4327-8f3a-6f72c8c15bc9" });

            migrationBuilder.DeleteData(
                table: "tbl_motor",
                keyColumn: "motor_id",
                keyValue: "05df018c-4e86-4327-8f3a-6f72c8c15bc9");

            migrationBuilder.DeleteData(
                table: "tbl_motor",
                keyColumn: "motor_id",
                keyValue: "dc1398ac-bf6b-4cd6-9d9b-b1a50c24ad65");

            migrationBuilder.DeleteData(
                table: "tbl_veicl",
                keyColumn: "veicl_id",
                keyValue: "8d93e52d-36df-4c6b-9505-239a9ac54966");

            migrationBuilder.DeleteData(
                table: "tbl_veicl",
                keyColumn: "veicl_id",
                keyValue: "bfd9f2db-032a-4dbe-8a07-019aab9f29ae");

            migrationBuilder.AlterColumn<string>(
                name: "cnh",
                table: "tbl_motor",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)");

            migrationBuilder.InsertData(
                table: "tbl_motor",
                columns: new[] { "motor_id", "ativo", "cnh", "nome", "validadeCNH" },
                values: new object[,]
                {
                    { "dd761061-f588-4cbc-a06e-d88f7559c4f0", true, "1234567891", "Samuel Almeida", new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "24ee7c1d-8969-4094-8ff4-92d4c3a30c1a", true, "9876549283", "João da Silva", new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "tbl_veicl",
                columns: new[] { "veicl_id", "ano", "modelo", "placa" },
                values: new object[,]
                {
                    { "fdf2bbd9-7cdf-4d71-98d2-94a32c9dd100", 2019, "Volvo FH 540", "ABC1234" },
                    { "0ba3f5d5-c6c4-485c-aa95-b0dd3e0625e3", 2018, "Scania R450", "CBA4321" }
                });

            migrationBuilder.InsertData(
                table: "tbl_motr_veicl",
                columns: new[] { "veicl_id", "motor_id" },
                values: new object[] { "fdf2bbd9-7cdf-4d71-98d2-94a32c9dd100", "dd761061-f588-4cbc-a06e-d88f7559c4f0" });

            migrationBuilder.InsertData(
                table: "tbl_motr_veicl",
                columns: new[] { "veicl_id", "motor_id" },
                values: new object[] { "0ba3f5d5-c6c4-485c-aa95-b0dd3e0625e3", "24ee7c1d-8969-4094-8ff4-92d4c3a30c1a" });
        }
    }
}
