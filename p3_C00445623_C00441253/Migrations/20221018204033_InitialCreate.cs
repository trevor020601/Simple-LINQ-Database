using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace p3_C00445623_C00441253.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Definitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    word = table.Column<string>(type: "TEXT", nullable: false),
                    definition = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Definitions",
                columns: new[] { "Id", "definition", "word" },
                values: new object[] { 1, "A mischievous person", "Rapscallion" });

            migrationBuilder.InsertData(
                table: "Definitions",
                columns: new[] { "Id", "definition", "word" },
                values: new object[] { 2, "A young and inexperienced person considered to be presumptuous or overconfident", "Whippersnapper" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Definitions");
        }
    }
}
