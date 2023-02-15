#nullable disable

namespace Storage.Core.EF.Migrations;

/// <inheritdoc />
public partial class CreateDbWithAllTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Authors",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("Id", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Topics",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("Id", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                AithorId = table.Column<Guid>(type: "TEXT", nullable: true),
                TopicId = table.Column<Guid>(type: "TEXT", nullable: true),
                AuthorId = table.Column<Guid>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("Id", x => x.Id);
                table.ForeignKey(
                    name: "FK_Books_Authors_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "Authors",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Books_Topics_TopicId",
                    column: x => x.TopicId,
                    principalTable: "Topics",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Books_AuthorId",
            table: "Books",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_Books_TopicId",
            table: "Books",
            column: "TopicId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Books");

        migrationBuilder.DropTable(
            name: "Authors");

        migrationBuilder.DropTable(
            name: "Topics");
    }
}
