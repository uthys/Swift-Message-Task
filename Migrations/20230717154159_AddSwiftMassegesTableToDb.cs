using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddSwiftMassegesTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SwiftMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Field1 = table.Column<string>(type: "TEXT", nullable: false),
                    Field2 = table.Column<string>(type: "TEXT", nullable: false),
                    Field4Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Field5Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwiftMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SwiftMessageChecksum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MAC = table.Column<string>(type: "TEXT", nullable: false),
                    CHK = table.Column<string>(type: "TEXT", nullable: false),
                    SwiftMessageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwiftMessageChecksum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SwiftMessageChecksum_SwiftMessage_SwiftMessageId",
                        column: x => x.SwiftMessageId,
                        principalTable: "SwiftMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SwiftMessageContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Field20 = table.Column<string>(type: "TEXT", nullable: false),
                    Field21 = table.Column<string>(type: "TEXT", nullable: false),
                    Field79 = table.Column<string>(type: "TEXT", nullable: false),
                    SwiftMessageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwiftMessageContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SwiftMessageContent_SwiftMessage_SwiftMessageId",
                        column: x => x.SwiftMessageId,
                        principalTable: "SwiftMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SwiftMessageChecksum_SwiftMessageId",
                table: "SwiftMessageChecksum",
                column: "SwiftMessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SwiftMessageContent_SwiftMessageId",
                table: "SwiftMessageContent",
                column: "SwiftMessageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SwiftMessageChecksum");

            migrationBuilder.DropTable(
                name: "SwiftMessageContent");

            migrationBuilder.DropTable(
                name: "SwiftMessage");
        }
    }
}
