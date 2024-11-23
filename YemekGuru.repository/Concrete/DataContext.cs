using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;

namespace YemekGuru.repository.Concrete;

public class DataContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<ApplySeller> ApplySellers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Complaint> Complaints { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\yusuf\\Desktop\\YemekGuru\\YemekGuru.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);
            
        modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant
            {
                Id = 1,
                Name = "Salon Burger",
                Description = "Lezzetli burgerlerle tanınan bir restoran.",
                ImageUrl = "1.jpeg",
                Country = "Turkey",
                CityId = 34,
                DistrictId = 2004,
                Address = "Örnek Caddesi No: 123",
                PhoneNumber = "+90 555 123 4567",
                EmailAdress = "info@salonburger.com",
                OpeningTime = "10:00",
                ClosingTime = "20:00",
                DeliveryTime = "30 dk",
                MinimumOrderPrice = 120.0f,
                Rating = 4.5f,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Restaurant
            {
                Id = 2,
                Name = "Pide Express",
                Description = "Harika pide çeşitleri sunan bir restoran.",
                ImageUrl = "2.jpeg",
                Country = "Turkey",
                CityId = 6,
                DistrictId = 1231,
                Address = "Örnek Sokak No: 456",
                PhoneNumber = "+90 555 987 6543",
                EmailAdress = "info@pideexpress.com",
                OpeningTime = "11:00",
                ClosingTime = "21:00",
                DeliveryTime = "25 dk",
                MinimumOrderPrice = 100.0f,
                Rating = 4.2f,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Restaurant
            {
                Id = 3,
                Name = "Usta Dönerci",
                Description = "Türkiyenin en sevilen dönercisi",
                ImageUrl = "3.jpeg",
                Country = "Turkey",
                CityId = 25,
                DistrictId = 1945,
                Address = "Örnek Sokak No: 456",
                PhoneNumber = "+90 535 896 14 55",
                EmailAdress = "info@ustadonerci.com",
                OpeningTime = "08:00",
                ClosingTime = "22:00",
                DeliveryTime = "40 dk",
                MinimumOrderPrice = 25.0f,
                Rating = 4.8f,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Döner",
                Description = "Lezzetli döner çeşitleri sunan kategori.",
                ImageUrl="1.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
                
            },
            new Category
            {
                Id = 2,
                Name = "Pizza",
                Description = "Farklı pizza lezzetlerini içeren kategori.",
                ImageUrl="2.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = 3,
                Name = "Burger",
                Description = "Farklı burger lezzetlerini içeren kategori.",
                ImageUrl="3.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = 4,
                Name = "Çiğ Köfte",
                Description = "Çeşitli çiğ köfte türlerini içeren kategori.",
                ImageUrl="4.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = 5,
                Name = "Kebap",
                Description = "Geleneksel kebap çeşitlerini içeren kategori.",
                ImageUrl="5.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = 6,
                Name = "Kanat",
                Description = "Lezzetli kanat çeşitleri içeren kategori.",
                ImageUrl="6.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = 7,
                Name = "Pide ve Lahmacun",
                Description = "Ev yapımı yemeklerin yer aldığı kategori.",
                ImageUrl="7.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = 8,
                Name = "Ev Yemeği",
                Description = "Ev yapımı yemeklerin yer aldığı kategori.",
                ImageUrl="8.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = 9,
                Name = "Vejeteryan",
                Description = "Vejetaryen lezzetlerin yer aldığı kategori.",
                ImageUrl="9.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = 10,
                Name = "Tatlı",
                Description = "Farklı tatlı çeşitlerini içeren kategori.",
                ImageUrl="10.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                RestaurantId = 1,
                CategoryId = 3,
                Name = "Klasik Burger",
                Description = "Sadece et, peynir ve özel sosla hazırlanan klasik burger.",
                Content = "Et, peynir, sos",
                Calorie = 500.0f,
                ImageUrl = "1.jpeg",
                PreviousPrice = 15,
                Price = 110,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now
            },
            new Product
            {
                Id = 2,
                RestaurantId = 1,
                CategoryId = 2,
                Name = "Karışık Pizza",
                Description = "Karışık malzemelerle hazırlanan enfes pizza.",
                Content = "Hamur, sos, peynir, sebzeler, et",
                Calorie = 800.0f,
                ImageUrl = "2.jpeg",
                PreviousPrice = 95,
                Price = 100,
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now
            },

            new Product
            {
                Id = 3,
                RestaurantId = 2,
                CategoryId = 7,
                Name = "Anadolu Pidesi",
                Description = "Taze malzemelerle hazırlanan nefis Anadolu pidesi.",
                Content = "Hamur, et, sebzeler",
                Calorie = 600.0f,
                ImageUrl = "3.jpeg",
                PreviousPrice = 110,
                Price = 100,
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now
            },
            new Product
            {
                Id = 4,
                RestaurantId = 2,
                CategoryId = 7,
                Name = "Karadeniz Pizzası",
                Description = "Karadeniz mutfağından esinlenilen lezzetli pizza.",
                Content = "Hamur, sos, peynir, hamsi, mısır",
                Calorie = 750.0f,
                ImageUrl = "4.jpeg",
                PreviousPrice = 105,
                Price = 110,
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now
            },
            new Product
            {
                Id = 5,
                RestaurantId = 3,
                CategoryId = 1, // Döner kategorisinin Id'si ile değiştirilmelidir
                Name = "Et Döner",
                Description = "Et döner ile hazırlanan nefis döner.",
                Content = "Et, baharatlar",
                Calorie = 600.0f,
                ImageUrl = "5.jpeg",
                PreviousPrice = 105,
                Price = 120,
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now
            },
            new Product
            {
                Id = 6,
                RestaurantId = 3,
                CategoryId = 1,
                Name = "Tavuk Döner",
                Description = "Tavuk eti ile hazırlanan lezzetli döner.",
                Content = "Tavuk eti, sos",
                Calorie = 500.0f,
                ImageUrl = "6.jpeg",
                PreviousPrice = 75,
                Price = 65,
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now
            },
            new Product
            {
                Id = 7,
                RestaurantId = 3,
                CategoryId = 1,
                Name = "Sebzeli Döner",
                Description = "Sebzelerle zenginleştirilmiş sağlıklı döner.",
                Content = "Et, sebzeler, sos",
                Calorie = 550.0f,
                ImageUrl = "https://example.com/sebzeli-doner.jpg",
                PreviousPrice = 90,
                Price = 100,
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now
            }
        );

        modelBuilder.Entity<City>().HasData(
            new City(){Id=25,Name="Erzurum",IsActive=true},
            new City(){Id=34,Name="İstanbul",IsActive=true}
        );
        modelBuilder.Entity<District>().HasData(
            // Erzurum İlçeleri
                new District { Id = 1153, CityId = 25, Name = "AŞKALE", IsActive = true },
                new District { Id = 1235, CityId = 25, Name = "ÇAT", IsActive = true },
                new District { Id = 1319, CityId = 25, Name = "MERKEZ", IsActive = true },
                new District { Id = 1392, CityId = 25, Name = "HINIS", IsActive = true },
                new District { Id = 1396, CityId = 25, Name = "HORASAN", IsActive = true },
                new District { Id = 1416, CityId = 25, Name = "İSPİR", IsActive = true },
                new District { Id = 1444, CityId = 25, Name = "KARAYAZI", IsActive = true },
                new District { Id = 1540, CityId = 25, Name = "NARMAN", IsActive = true },
                new District { Id = 1550, CityId = 25, Name = "OLTU", IsActive = true },
                new District { Id = 1551, CityId = 25, Name = "OLUR", IsActive = true },
                new District { Id = 1567, CityId = 25, Name = "PASİNLER", IsActive = true },
                new District { Id = 1657, CityId = 25, Name = "ŞENKAYA", IsActive = true },
                new District { Id = 1674, CityId = 25, Name = "TEKMAN", IsActive = true },
                new District { Id = 1683, CityId = 25, Name = "TORTUM", IsActive = true },
                new District { Id = 1812, CityId = 25, Name = "KARAÇOBAN", IsActive = true },
                new District { Id = 1851, CityId = 25, Name = "UZUNDERE", IsActive = true },
                new District { Id = 1865, CityId = 25, Name = "PAZARYOLU", IsActive = true },
                new District { Id = 1945, CityId = 25, Name = "AZİZİYE", IsActive = true },
                new District { Id = 1967, CityId = 25, Name = "KÖPRÜKÖY", IsActive = true },
                new District { Id = 2044, CityId = 25, Name = "PALANDÖKEN", IsActive = true },
                new District { Id = 2045, CityId = 25, Name = "YAKUTİYE", IsActive = true },


            // İstanbul İlçeleri
                new District { Id = 1103, CityId = 34, Name = "ADALAR", IsActive = true },
                new District { Id = 1166, CityId = 34, Name = "BAKIRKÖY", IsActive = true },
                new District { Id = 1183, CityId = 34, Name = "BEŞİKTAŞ", IsActive = true },
                new District { Id = 1185, CityId = 34, Name = "BEYKOZ", IsActive = true },
                new District { Id = 1186, CityId = 34, Name = "BEYOĞLU", IsActive = true },
                new District { Id = 1237, CityId = 34, Name = "ÇATALCA", IsActive = true },
                new District { Id = 1325, CityId = 34, Name = "EYÜP", IsActive = true },
                new District { Id = 1327, CityId = 34, Name = "FATİH", IsActive = true },
                new District { Id = 1336, CityId = 34, Name = "GAZİOSMANPAŞA", IsActive = true },
                new District { Id = 1421, CityId = 34, Name = "KADIKÖY", IsActive = true },
                new District { Id = 1449, CityId = 34, Name = "KARTAL", IsActive = true },
                new District { Id = 1604, CityId = 34, Name = "SARIYER", IsActive = true },
                new District { Id = 1622, CityId = 34, Name = "SİLİVRİ", IsActive = true },
                new District { Id = 1659, CityId = 34, Name = "ŞİLE", IsActive = true },
                new District { Id = 1663, CityId = 34, Name = "ŞİŞLİ", IsActive = true },
                new District { Id = 1708, CityId = 34, Name = "ÜSKÜDAR", IsActive = true },
                new District { Id = 1739, CityId = 34, Name = "ZEYTİNBURNU", IsActive = true },
                new District { Id = 1782, CityId = 34, Name = "BÜYÜKÇEKMECE", IsActive = true },
                new District { Id = 1810, CityId = 34, Name = "KAĞITHANE", IsActive = true },
                new District { Id = 1823, CityId = 34, Name = "KÜÇÜKÇEKMECE", IsActive = true },
                new District { Id = 1835, CityId = 34, Name = "PENDİK", IsActive = true },
                new District { Id = 1852, CityId = 34, Name = "ÜMRANİYE", IsActive = true },
                new District { Id = 1886, CityId = 34, Name = "BAYRAMPAŞA", IsActive = true },
                new District { Id = 2003, CityId = 34, Name = "AVCILAR", IsActive = true },
                new District { Id = 2004, CityId = 34, Name = "BAĞCILAR", IsActive = true },
                new District { Id = 2005, CityId = 34, Name = "BAHÇELİEVLER", IsActive = true },
                new District { Id = 2010, CityId = 34, Name = "GÜNGÖREN", IsActive = true },
                new District { Id = 2012, CityId = 34, Name = "MALTEPE", IsActive = true },
                new District { Id = 2014, CityId = 34, Name = "SULTANBEYLİ", IsActive = true },
                new District { Id = 2015, CityId = 34, Name = "TUZLA", IsActive = true },
                new District { Id = 2016, CityId = 34, Name = "ESENLER", IsActive = true },
                new District { Id = 2048, CityId = 34, Name = "ARNAVUTKÖY", IsActive = true },
                new District { Id = 2049, CityId = 34, Name = "ATAŞEHİR", IsActive = true },
                new District { Id = 2050, CityId = 34, Name = "BAŞAKŞEHİR", IsActive = true },
                new District { Id = 2051, CityId = 34, Name = "BEYLİKDÜZÜ", IsActive = true },
                new District { Id = 2052, CityId = 34, Name = "ÇEKMEKÖY", IsActive = true },
                new District { Id = 2053, CityId = 34, Name = "ESENYURT", IsActive = true },
                new District { Id = 2054, CityId = 34, Name = "SANCAKTEPE", IsActive = true },
                new District { Id = 2055, CityId = 34, Name = "SULTANGAZİ", IsActive = true }
        );
    }

}