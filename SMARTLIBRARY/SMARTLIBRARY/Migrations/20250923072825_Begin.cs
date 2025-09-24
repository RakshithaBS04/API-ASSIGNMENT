using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SMARTLIBRARY.Migrations
{
    /// <inheritdoc />
    public partial class Begin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceCategories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadedById = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_ResourceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ResourceCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PdfResources",
                columns: table => new
                {
                    ResourceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadedById = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfResources", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_PdfResources_ResourceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ResourceCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PdfResources_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceAccessLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    BookId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResourceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceAccessLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_ResourceAccessLogs_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceAccessLogs_PdfResources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "PdfResources",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceAccessLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ResourceCategories",
                columns: new[] { "CategoryId", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { "C1", "Science related resources", true, "Science" },
                    { "C2", "Technology related resources", true, "Technology" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, "System administrator", "Admin" },
                    { 2, "Manages library resources", "Librarian" },
                    { 3, "Student user", "Student" },
                    { 4, "Faculty user", "Faculty" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "FullName", "ImageUrl", "IsActive", "Password", "RoleId" },
                values: new object[,]
                {
                    { "ADM001", new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@smartlib.com", "Super Admin", "/Images/admin.jpg", true, "admin123", 1 },
                    { "KAF001", new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "faculty@smartlib.com", "Test Faculty", "/Images/faculty.jpg", true, "fac123", 4 },
                    { "KAS001", new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "student@smartlib.com", "Test Student", "/Images/student.png", true, "stud123", 3 },
                    { "LIB001", new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "librarian@smartlib.com", "Main Librarian", "/Images/librarian.png", true, "lib123", 2 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "CategoryId", "FilePath", "ISBN", "ImageUrl", "IsActive", "Title", "UploadedAt", "UploadedById" },
                values: new object[] { "B1", "Dr. Newton", "C1", "/pdfs/books/physics.pdf", "123456789", "/Images/books/physics.png", true, "Physics Fundamentals", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "LIB001" });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "NotificationId", "CreatedAt", "IsRead", "Message", "RecipientId", "Title" },
                values: new object[] { 1, new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Welcome to Smart Library System!", "KAS001", "Welcome" });

            migrationBuilder.InsertData(
                table: "PdfResources",
                columns: new[] { "ResourceId", "CategoryId", "FilePath", "IsActive", "ResourceType", "Title", "UploadedAt", "UploadedById" },
                values: new object[] { "R1", "C2", "/pdfs/papers/ai_paper.pdf", true, "IEEE Paper", "AI Research Paper", new DateTime(2025, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "KAF001" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UploadedById",
                table: "Books",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientId",
                table: "Notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_PdfResources_CategoryId",
                table: "PdfResources",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PdfResources_UploadedById",
                table: "PdfResources",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAccessLogs_BookId",
                table: "ResourceAccessLogs",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAccessLogs_ResourceId",
                table: "ResourceAccessLogs",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAccessLogs_UserId",
                table: "ResourceAccessLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ResourceAccessLogs");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "PdfResources");

            migrationBuilder.DropTable(
                name: "ResourceCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
