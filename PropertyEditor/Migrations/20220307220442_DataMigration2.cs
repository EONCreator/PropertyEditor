using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PropertyEditor.Migrations
{
    public partial class DataMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntegerProperties_Objects_ObjectId",
                table: "IntegerProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegerProperties_Properties_PropertyId",
                table: "IntegerProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_StringProperties_Objects_ObjectId",
                table: "StringProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_StringProperties_Properties_PropertyId",
                table: "StringProperties");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_StringProperties_ObjectId",
                table: "StringProperties");

            migrationBuilder.DropIndex(
                name: "IX_StringProperties_PropertyId",
                table: "StringProperties");

            migrationBuilder.DropIndex(
                name: "IX_IntegerProperties_ObjectId",
                table: "IntegerProperties");

            migrationBuilder.DropIndex(
                name: "IX_IntegerProperties_PropertyId",
                table: "IntegerProperties");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "StringProperties");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "StringProperties");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "IntegerProperties");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "IntegerProperties");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "IntegerProperties");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "StringProperties",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "IntegerProperties",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IntegerValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PropertyId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    ObjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegerValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegerValues_Objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntegerValues_IntegerProperties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "IntegerProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StringValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PropertyId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ObjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringValues_Objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StringValues_StringProperties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "StringProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntegerValues_ObjectId",
                table: "IntegerValues",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerValues_PropertyId",
                table: "IntegerValues",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_StringValues_ObjectId",
                table: "StringValues",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StringValues_PropertyId",
                table: "StringValues",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegerValues");

            migrationBuilder.DropTable(
                name: "StringValues");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "IntegerProperties");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StringProperties",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "StringProperties",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "StringProperties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "IntegerProperties",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "IntegerProperties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "IntegerProperties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StringProperties_ObjectId",
                table: "StringProperties",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StringProperties_PropertyId",
                table: "StringProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerProperties_ObjectId",
                table: "IntegerProperties",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerProperties_PropertyId",
                table: "IntegerProperties",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntegerProperties_Objects_ObjectId",
                table: "IntegerProperties",
                column: "ObjectId",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegerProperties_Properties_PropertyId",
                table: "IntegerProperties",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StringProperties_Objects_ObjectId",
                table: "StringProperties",
                column: "ObjectId",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StringProperties_Properties_PropertyId",
                table: "StringProperties",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
