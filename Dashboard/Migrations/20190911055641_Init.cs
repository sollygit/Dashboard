using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDepot = table.Column<bool>(nullable: false),
                    TradingAs = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryOrders",
                columns: table => new
                {
                    DeliveryOrderId = table.Column<string>(nullable: false),
                    TransCode = table.Column<string>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    RequestDate = table.Column<DateTimeOffset>(nullable: false),
                    PickupDateTime = table.Column<DateTimeOffset>(nullable: true),
                    DeliveryDateTime = table.Column<DateTimeOffset>(nullable: true),
                    FulfilmentType = table.Column<string>(nullable: true),
                    SourceId = table.Column<string>(nullable: true),
                    HoldReleaseFlag = table.Column<bool>(nullable: false),
                    CustomerPromise = table.Column<string>(nullable: true),
                    PickStatus = table.Column<string>(nullable: true),
                    PickStatusCompleteDateTime = table.Column<DateTimeOffset>(nullable: true),
                    OMUAppPacked = table.Column<bool>(nullable: false),
                    PickArea = table.Column<string>(nullable: true),
                    Weight = table.Column<decimal>(nullable: false),
                    SpareField = table.Column<string>(nullable: true),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    DeliveryStatus = table.Column<string>(nullable: true),
                    TruckNumber = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOrders", x => new { x.DeliveryOrderId, x.TransCode, x.BranchId });
                    table.ForeignKey(
                        name: "FK_DeliveryOrders_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    LineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineNumber = table.Column<int>(nullable: false),
                    Sku = table.Column<string>(nullable: true),
                    SpecialOrder = table.Column<bool>(nullable: false),
                    Substitution = table.Column<bool>(nullable: false),
                    BackOrder = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Weight = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    StockOnHand = table.Column<decimal>(nullable: false),
                    QuantityPicked = table.Column<decimal>(nullable: false),
                    Picker = table.Column<string>(nullable: true),
                    DeliveryOrderId = table.Column<string>(nullable: true),
                    DeliveryOrderTransCode = table.Column<string>(nullable: true),
                    DeliveryOrderBranchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.LineId);
                    table.ForeignKey(
                        name: "FK_Lines_DeliveryOrders_DeliveryOrderId_DeliveryOrderTransCode_DeliveryOrderBranchId",
                        columns: x => new { x.DeliveryOrderId, x.DeliveryOrderTransCode, x.DeliveryOrderBranchId },
                        principalTable: "DeliveryOrders",
                        principalColumns: new[] { "DeliveryOrderId", "TransCode", "BranchId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageNotes",
                columns: table => new
                {
                    PackageNoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Packaging = table.Column<string>(nullable: true),
                    StagingArea = table.Column<string>(nullable: true),
                    Packer = table.Column<string>(nullable: true),
                    DeliveryOrderId = table.Column<string>(nullable: true),
                    DeliveryOrderTransCode = table.Column<string>(nullable: true),
                    DeliveryOrderBranchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageNotes", x => x.PackageNoteId);
                    table.ForeignKey(
                        name: "FK_PackageNotes_DeliveryOrders_DeliveryOrderId_DeliveryOrderTransCode_DeliveryOrderBranchId",
                        columns: x => new { x.DeliveryOrderId, x.DeliveryOrderTransCode, x.DeliveryOrderBranchId },
                        principalTable: "DeliveryOrders",
                        principalColumns: new[] { "DeliveryOrderId", "TransCode", "BranchId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pickers",
                columns: table => new
                {
                    PickerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DeliveryOrderId = table.Column<string>(nullable: true),
                    DeliveryOrderTransCode = table.Column<string>(nullable: true),
                    DeliveryOrderBranchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pickers", x => x.PickerId);
                    table.ForeignKey(
                        name: "FK_Pickers_DeliveryOrders_DeliveryOrderId_DeliveryOrderTransCode_DeliveryOrderBranchId",
                        columns: x => new { x.DeliveryOrderId, x.DeliveryOrderTransCode, x.DeliveryOrderBranchId },
                        principalTable: "DeliveryOrders",
                        principalColumns: new[] { "DeliveryOrderId", "TransCode", "BranchId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrders_LocationId",
                table: "DeliveryOrders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_DeliveryOrderId_DeliveryOrderTransCode_DeliveryOrderBranchId",
                table: "Lines",
                columns: new[] { "DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_PackageNotes_DeliveryOrderId_DeliveryOrderTransCode_DeliveryOrderBranchId",
                table: "PackageNotes",
                columns: new[] { "DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_Pickers_DeliveryOrderId_DeliveryOrderTransCode_DeliveryOrderBranchId",
                table: "Pickers",
                columns: new[] { "DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "PackageNotes");

            migrationBuilder.DropTable(
                name: "Pickers");

            migrationBuilder.DropTable(
                name: "DeliveryOrders");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
