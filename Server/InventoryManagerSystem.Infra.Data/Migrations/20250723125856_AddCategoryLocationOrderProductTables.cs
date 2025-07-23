using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InventoryManagerSystem.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryLocationOrderProductTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "ActivityTracker",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //         Title = table.Column<string>(type: "text", nullable: true),
            //         Description = table.Column<string>(type: "text", nullable: true),
            //         OperationState = table.Column<bool>(type: "boolean", nullable: false),
            //         UserId = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_ActivityTracker", x => x.Id);
            //     });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    ctg_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ctg_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.ctg_id);
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    ltn_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ltn_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.ltn_id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    pdt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pdt_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    pdt_serial_number = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    pdt_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    pdt_quantity = table.Column<int>(type: "integer", nullable: false),
                    pdt_description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    pdt_base64_image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    pdt_date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ctg_id = table.Column<int>(type: "integer", nullable: false),
                    ltn_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.pdt_id);
                    table.ForeignKey(
                        name: "FK_product_category_ctg_id",
                        column: x => x.ctg_id,
                        principalTable: "category",
                        principalColumn: "ctg_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_location_ltn_id",
                        column: x => x.ltn_id,
                        principalTable: "location",
                        principalColumn: "ltn_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    ord_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ord_date_ordered = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ord_delivering_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ord_quantity = table.Column<int>(type: "integer", nullable: false),
                    ord_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    ord_total_amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    ord_order_state = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    pdt_id = table.Column<int>(type: "integer", nullable: false),
                    usr_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.ord_id);
                    table.ForeignKey(
                        name: "FK_order_product_pdt_id",
                        column: x => x.pdt_id,
                        principalTable: "product",
                        principalColumn: "pdt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_ctg_name",
                table: "category",
                column: "ctg_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_location_ltn_name",
                table: "location",
                column: "ltn_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_pdt_id",
                table: "order",
                column: "pdt_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_ctg_id",
                table: "product",
                column: "ctg_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_ltn_id",
                table: "product",
                column: "ltn_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_pdt_name",
                table: "product",
                column: "pdt_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropTable(
            //     name: "ActivityTracker");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "location");
        }
    }
}
