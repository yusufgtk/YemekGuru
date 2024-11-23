using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YemekGuru.repository.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<int>(type: "INTEGER", nullable: true),
                    District = table.Column<int>(type: "INTEGER", nullable: true),
                    FullAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplySellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    TCKN = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    BirthDay = table.Column<string>(type: "TEXT", nullable: true),
                    RestaurantName = table.Column<string>(type: "TEXT", nullable: true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    RestaurantPhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    MinimumOrderPrice = table.Column<int>(type: "INTEGER", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    DistrictId = table.Column<int>(type: "INTEGER", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: true),
                    ApplyState = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplySellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    RestaurantName = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    ProductTypesNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    DistrictId = table.Column<int>(type: "INTEGER", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    CommentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    DistrictId = table.Column<int>(type: "INTEGER", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    EmailAdress = table.Column<string>(type: "TEXT", nullable: true),
                    OpeningTime = table.Column<string>(type: "TEXT", nullable: true),
                    ClosingTime = table.Column<string>(type: "TEXT", nullable: true),
                    DeliveryTime = table.Column<string>(type: "TEXT", nullable: true),
                    MinimumOrderPrice = table.Column<float>(type: "REAL", nullable: true),
                    Rating = table.Column<float>(type: "REAL", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complaints_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Calorie = table.Column<float>(type: "REAL", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    PreviousPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    CartId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(783), "Lezzetli döner çeşitleri sunan kategori.", "1.jpeg", "Döner", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(784) },
                    { 2, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(785), "Farklı pizza lezzetlerini içeren kategori.", "2.jpeg", "Pizza", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(786) },
                    { 3, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(787), "Farklı burger lezzetlerini içeren kategori.", "3.jpeg", "Burger", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(788) },
                    { 4, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(789), "Çeşitli çiğ köfte türlerini içeren kategori.", "4.jpeg", "Çiğ Köfte", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(790) },
                    { 5, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(791), "Geleneksel kebap çeşitlerini içeren kategori.", "5.jpeg", "Kebap", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(791) },
                    { 6, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(792), "Lezzetli kanat çeşitleri içeren kategori.", "6.jpeg", "Kanat", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(793) },
                    { 7, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(795), "Ev yapımı yemeklerin yer aldığı kategori.", "7.jpeg", "Pide ve Lahmacun", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(796) },
                    { 8, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(797), "Ev yapımı yemeklerin yer aldığı kategori.", "8.jpeg", "Ev Yemeği", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(797) },
                    { 9, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(799), "Vejetaryen lezzetlerin yer aldığı kategori.", "9.jpeg", "Vejeteryan", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(799) },
                    { 10, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(801), "Farklı tatlı çeşitlerini içeren kategori.", "10.jpeg", "Tatlı", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(802) }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 25, true, "Erzurum" },
                    { 34, true, "İstanbul" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CityId", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1103, 34, true, "ADALAR" },
                    { 1153, 25, true, "AŞKALE" },
                    { 1166, 34, true, "BAKIRKÖY" },
                    { 1183, 34, true, "BEŞİKTAŞ" },
                    { 1185, 34, true, "BEYKOZ" },
                    { 1186, 34, true, "BEYOĞLU" },
                    { 1235, 25, true, "ÇAT" },
                    { 1237, 34, true, "ÇATALCA" },
                    { 1319, 25, true, "MERKEZ" },
                    { 1325, 34, true, "EYÜP" },
                    { 1327, 34, true, "FATİH" },
                    { 1336, 34, true, "GAZİOSMANPAŞA" },
                    { 1392, 25, true, "HINIS" },
                    { 1396, 25, true, "HORASAN" },
                    { 1416, 25, true, "İSPİR" },
                    { 1421, 34, true, "KADIKÖY" },
                    { 1444, 25, true, "KARAYAZI" },
                    { 1449, 34, true, "KARTAL" },
                    { 1540, 25, true, "NARMAN" },
                    { 1550, 25, true, "OLTU" },
                    { 1551, 25, true, "OLUR" },
                    { 1567, 25, true, "PASİNLER" },
                    { 1604, 34, true, "SARIYER" },
                    { 1622, 34, true, "SİLİVRİ" },
                    { 1657, 25, true, "ŞENKAYA" },
                    { 1659, 34, true, "ŞİLE" },
                    { 1663, 34, true, "ŞİŞLİ" },
                    { 1674, 25, true, "TEKMAN" },
                    { 1683, 25, true, "TORTUM" },
                    { 1708, 34, true, "ÜSKÜDAR" },
                    { 1739, 34, true, "ZEYTİNBURNU" },
                    { 1782, 34, true, "BÜYÜKÇEKMECE" },
                    { 1810, 34, true, "KAĞITHANE" },
                    { 1812, 25, true, "KARAÇOBAN" },
                    { 1823, 34, true, "KÜÇÜKÇEKMECE" },
                    { 1835, 34, true, "PENDİK" },
                    { 1851, 25, true, "UZUNDERE" },
                    { 1852, 34, true, "ÜMRANİYE" },
                    { 1865, 25, true, "PAZARYOLU" },
                    { 1886, 34, true, "BAYRAMPAŞA" },
                    { 1945, 25, true, "AZİZİYE" },
                    { 1967, 25, true, "KÖPRÜKÖY" },
                    { 2003, 34, true, "AVCILAR" },
                    { 2004, 34, true, "BAĞCILAR" },
                    { 2005, 34, true, "BAHÇELİEVLER" },
                    { 2010, 34, true, "GÜNGÖREN" },
                    { 2012, 34, true, "MALTEPE" },
                    { 2014, 34, true, "SULTANBEYLİ" },
                    { 2015, 34, true, "TUZLA" },
                    { 2016, 34, true, "ESENLER" },
                    { 2044, 25, true, "PALANDÖKEN" },
                    { 2045, 25, true, "YAKUTİYE" },
                    { 2048, 34, true, "ARNAVUTKÖY" },
                    { 2049, 34, true, "ATAŞEHİR" },
                    { 2050, 34, true, "BAŞAKŞEHİR" },
                    { 2051, 34, true, "BEYLİKDÜZÜ" },
                    { 2052, 34, true, "ÇEKMEKÖY" },
                    { 2053, 34, true, "ESENYURT" },
                    { 2054, 34, true, "SANCAKTEPE" },
                    { 2055, 34, true, "SULTANGAZİ" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "CityId", "ClosingTime", "Country", "CreatedAt", "DeliveryTime", "Description", "DistrictId", "EmailAdress", "ImageUrl", "IsActive", "IsApproved", "MinimumOrderPrice", "Name", "OpeningTime", "PhoneNumber", "Rating", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Örnek Caddesi No: 123", 34, "20:00", "Turkey", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(648), "30 dk", "Lezzetli burgerlerle tanınan bir restoran.", 2004, "info@salonburger.com", "1.jpeg", true, null, 120f, "Salon Burger", "10:00", "+90 555 123 4567", 4.5f, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(662), null },
                    { 2, "Örnek Sokak No: 456", 6, "21:00", "Turkey", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(667), "25 dk", "Harika pide çeşitleri sunan bir restoran.", 1231, "info@pideexpress.com", "2.jpeg", true, null, 100f, "Pide Express", "11:00", "+90 555 987 6543", 4.2f, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(667), null },
                    { 3, "Örnek Sokak No: 456", 25, "22:00", "Turkey", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(671), "40 dk", "Türkiyenin en sevilen dönercisi", 1945, "info@ustadonerci.com", "3.jpeg", true, null, 25f, "Usta Dönerci", "08:00", "+90 535 896 14 55", 4.8f, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(671), null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Calorie", "CategoryId", "Content", "CreatedTime", "Description", "ImageUrl", "IsActive", "IsApproved", "Name", "PreviousPrice", "Price", "RestaurantId", "UpdateTime" },
                values: new object[,]
                {
                    { 1, 500f, 3, "Et, peynir, sos", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(832), "Sadece et, peynir ve özel sosla hazırlanan klasik burger.", "1.jpeg", false, false, "Klasik Burger", 15m, 110m, 1, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(833) },
                    { 2, 800f, 2, "Hamur, sos, peynir, sebzeler, et", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(836), "Karışık malzemelerle hazırlanan enfes pizza.", "2.jpeg", true, false, "Karışık Pizza", 95m, 100m, 1, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(837) },
                    { 3, 600f, 7, "Hamur, et, sebzeler", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(839), "Taze malzemelerle hazırlanan nefis Anadolu pidesi.", "3.jpeg", true, false, "Anadolu Pidesi", 110m, 100m, 2, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(840) },
                    { 4, 750f, 7, "Hamur, sos, peynir, hamsi, mısır", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(843), "Karadeniz mutfağından esinlenilen lezzetli pizza.", "4.jpeg", true, false, "Karadeniz Pizzası", 105m, 110m, 2, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(843) },
                    { 5, 600f, 1, "Et, baharatlar", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(846), "Et döner ile hazırlanan nefis döner.", "5.jpeg", true, false, "Et Döner", 105m, 120m, 3, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(846) },
                    { 6, 500f, 1, "Tavuk eti, sos", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(849), "Tavuk eti ile hazırlanan lezzetli döner.", "6.jpeg", true, false, "Tavuk Döner", 75m, 65m, 3, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(849) },
                    { 7, 550f, 1, "Et, sebzeler, sos", new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(852), "Sebzelerle zenginleştirilmiş sağlıklı döner.", "https://example.com/sebzeli-doner.jpg", true, false, "Sebzeli Döner", 90m, 100m, 3, new DateTime(2024, 3, 12, 14, 32, 10, 108, DateTimeKind.Local).AddTicks(852) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OrderId1",
                table: "Comments",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RestaurantId",
                table: "Comments",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_OrderId",
                table: "Complaints",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_RestaurantId",
                table: "Products",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ApplySellers");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
