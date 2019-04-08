using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackMyShipment.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Carriers",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Cost = table.Column<long>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Carriers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Company",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Company", x => x.Id); });

            migrationBuilder.CreateTable(
                "Roles",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Roles", x => x.Id); });

            migrationBuilder.CreateTable(
                "Subscriptions",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Subscriptions", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    SubscriptionId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        "FK_Users_Company_CompanyId",
                        x => x.CompanyId,
                        "Company",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Users_Roles_RoleId",
                        x => x.RoleId,
                        "Roles",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Users_Subscriptions_SubscriptionId",
                        x => x.SubscriptionId,
                        "Subscriptions",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Address",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetLine1 = table.Column<string>(nullable: true),
                    StreetLine2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    UsersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        "FK_Address_Users_UsersId",
                        x => x.UsersId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Supplies",
                table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    CarrierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplies", x => new {x.UserId, x.CarrierId});
                    table.ForeignKey(
                        "FK_Supplies_Carriers_CarrierId",
                        x => x.CarrierId,
                        "Carriers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Supplies_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "Roles",
                new[] {"Id", "Name"},
                new object[,]
                {
                    {1, "admin"},
                    {2, "customer"},
                    {3, "carrier"}
                });

            migrationBuilder.InsertData(
                "Subscriptions",
                new[] {"Id", "Status"},
                new object[,]
                {
                    {1, "free"},
                    {2, "paid"}
                });

            migrationBuilder.CreateIndex(
                "IX_Address_UsersId",
                "Address",
                "UsersId");

            migrationBuilder.CreateIndex(
                "IX_Supplies_CarrierId",
                "Supplies",
                "CarrierId");

            migrationBuilder.CreateIndex(
                "IX_Users_CompanyId",
                "Users",
                "CompanyId");

            migrationBuilder.CreateIndex(
                "IX_Users_Email",
                "Users",
                "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Users_RoleId",
                "Users",
                "RoleId");

            migrationBuilder.CreateIndex(
                "IX_Users_SubscriptionId",
                "Users",
                "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Address");

            migrationBuilder.DropTable(
                "Supplies");

            migrationBuilder.DropTable(
                "Carriers");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Company");

            migrationBuilder.DropTable(
                "Roles");

            migrationBuilder.DropTable(
                "Subscriptions");
        }
    }
}