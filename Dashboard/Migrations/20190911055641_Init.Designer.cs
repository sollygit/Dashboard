﻿// <auto-generated />
using System;
using Dashboard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dashboard.Migrations
{
    [DbContext(typeof(DashboardContext))]
    [Migration("20190911055641_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dashboard.Models.DeliveryOrder", b =>
                {
                    b.Property<string>("DeliveryOrderId");

                    b.Property<string>("TransCode");

                    b.Property<int>("BranchId");

                    b.Property<string>("CustomerId");

                    b.Property<string>("CustomerPromise");

                    b.Property<string>("DeliveryAddress");

                    b.Property<DateTimeOffset?>("DeliveryDateTime");

                    b.Property<string>("DeliveryStatus");

                    b.Property<string>("FulfilmentType");

                    b.Property<bool>("HoldReleaseFlag");

                    b.Property<DateTimeOffset>("LastUpdated");

                    b.Property<int?>("LocationId");

                    b.Property<bool>("OMUAppPacked");

                    b.Property<string>("PickArea");

                    b.Property<string>("PickStatus");

                    b.Property<DateTimeOffset?>("PickStatusCompleteDateTime");

                    b.Property<DateTimeOffset?>("PickupDateTime");

                    b.Property<DateTimeOffset>("RequestDate");

                    b.Property<string>("SourceId");

                    b.Property<string>("SpareField");

                    b.Property<string>("TruckNumber");

                    b.Property<decimal>("Weight");

                    b.HasKey("DeliveryOrderId", "TransCode", "BranchId");

                    b.HasIndex("LocationId");

                    b.ToTable("DeliveryOrders");
                });

            modelBuilder.Entity("Dashboard.Models.Line", b =>
                {
                    b.Property<int>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BackOrder");

                    b.Property<int?>("DeliveryOrderBranchId");

                    b.Property<string>("DeliveryOrderId");

                    b.Property<string>("DeliveryOrderTransCode");

                    b.Property<string>("Description");

                    b.Property<int>("LineNumber");

                    b.Property<string>("Picker");

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("QuantityPicked");

                    b.Property<string>("Sku");

                    b.Property<bool>("SpecialOrder");

                    b.Property<decimal>("StockOnHand");

                    b.Property<bool>("Substitution");

                    b.Property<decimal>("Weight");

                    b.HasKey("LineId");

                    b.HasIndex("DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("Dashboard.Models.Location", b =>
                {
                    b.Property<int>("LocationId");

                    b.Property<bool>("IsDepot");

                    b.Property<string>("Name");

                    b.Property<string>("TradingAs");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Dashboard.Models.PackageNote", b =>
                {
                    b.Property<int>("PackageNoteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeliveryOrderBranchId");

                    b.Property<string>("DeliveryOrderId");

                    b.Property<string>("DeliveryOrderTransCode");

                    b.Property<string>("Packaging");

                    b.Property<string>("Packer");

                    b.Property<string>("StagingArea");

                    b.HasKey("PackageNoteId");

                    b.HasIndex("DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId");

                    b.ToTable("PackageNotes");
                });

            modelBuilder.Entity("Dashboard.Models.Picker", b =>
                {
                    b.Property<int>("PickerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeliveryOrderBranchId");

                    b.Property<string>("DeliveryOrderId");

                    b.Property<string>("DeliveryOrderTransCode");

                    b.Property<string>("Name");

                    b.HasKey("PickerId");

                    b.HasIndex("DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId");

                    b.ToTable("Pickers");
                });

            modelBuilder.Entity("Dashboard.Models.DeliveryOrder", b =>
                {
                    b.HasOne("Dashboard.Models.Location", "Location")
                        .WithMany("DeliveryOrders")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Dashboard.Models.Line", b =>
                {
                    b.HasOne("Dashboard.Models.DeliveryOrder", "DeliveryOrder")
                        .WithMany("Lines")
                        .HasForeignKey("DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Models.PackageNote", b =>
                {
                    b.HasOne("Dashboard.Models.DeliveryOrder", "DeliveryOrder")
                        .WithMany("PackageNotes")
                        .HasForeignKey("DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Models.Picker", b =>
                {
                    b.HasOne("Dashboard.Models.DeliveryOrder", "DeliveryOrder")
                        .WithMany("Pickers")
                        .HasForeignKey("DeliveryOrderId", "DeliveryOrderTransCode", "DeliveryOrderBranchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
