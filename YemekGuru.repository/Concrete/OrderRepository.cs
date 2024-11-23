using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class OrderRepository : Repository<Order, DataContext>, IOrderRepository
{
    public async Task<List<Order>> GetActiveOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders
                .Include(o => o.OrderItems) // Eğer OrderItems'i de almak istiyorsanız
                .ThenInclude(oi => oi.Product)
                .Where(o => o.RestaurantId == restaurantId&&o.Status==1)
                .OrderBy(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<List<Order>> GetCanceledOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders
                .Include(o => o.OrderItems) // Eğer OrderItems'i de almak istiyorsanız
                .ThenInclude(oi => oi.Product)
                .Where(o => o.RestaurantId == restaurantId&&o.Status==-1)
                .OrderByDescending(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<List<Order>> GetComplatedOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders
                .Include(o => o.OrderItems) // Eğer OrderItems'i de almak istiyorsanız
                .ThenInclude(oi => oi.Product)
                .Where(o => o.RestaurantId == restaurantId&&o.Status==3)
                .OrderByDescending(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<Order> GetOrderByIdAsync(int? id)
    {
        using(var context = new DataContext())
        {
            return await context.Orders
                .Include(o => o.OrderItems) // Eğer OrderItems'i de almak istiyorsanız
                .ThenInclude(p=>p.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }

    public async Task<List<Order>> GetOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders
                .Include(o => o.OrderItems) // Eğer OrderItems'i de almak istiyorsanız
                .ThenInclude(oi => oi.Product)
                .Where(o => o.RestaurantId == restaurantId)
                .OrderBy(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders
                .Include(o => o.OrderItems) // Eğer OrderItems'i de almak istiyorsanız
                .ThenInclude(oi => oi.Product) // OrderItems içindeki Product'ları dahil et
                .Where(o => o.UserId == userId)
                .OrderByDescending(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<List<Order>> GetSetOutOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders
                .Include(o => o.OrderItems) // Eğer OrderItems'i de almak istiyorsanız
                .ThenInclude(oi => oi.Product)
                .Where(o => o.RestaurantId == restaurantId&&o.Status==2)
                .OrderBy(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<int> GetTotalActiveOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o => o.RestaurantId == restaurantId&&o.Status==1).CountAsync();
        }
    }

    public async Task<int> GetTotalCanceledOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o => o.RestaurantId == restaurantId&&o.Status==-1).CountAsync();
        }
    }

    public async Task<int> GetTotalComplatedOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o => o.RestaurantId == restaurantId&&o.Status==3).CountAsync();
        }
    }

    public async Task<List<Order>> GetCompletedOrderByRestaurantIdAndOrderIdAsync(int? restaurantId, int? orderId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o => o.RestaurantId == restaurantId&&o.Id==orderId&&o.Status==3).ToListAsync();
        }
    }

    public async Task<int> GetTotalOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o => o.RestaurantId == restaurantId).CountAsync();
        }
    }

    public async Task<int> GetTotalSetOutOrderByRestaurantIdAsync(int? restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o => o.RestaurantId == restaurantId&&o.Status==2).CountAsync();
        }
    }

    public async Task<int> Admin_TotalOrdersCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Orders.CountAsync();
        }
    }

    public async Task<int> Admin_TotalCompletedOrdersCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o =>o.Status==3).CountAsync();
        }
    }

    public async Task<int> Admin_TotalCanceledOrdersCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o =>o.Status==-1).CountAsync();
        }
    }

    public async Task<int> Admin_TotalSetOutOrdersCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o =>o.Status==2).CountAsync();
        }
    }

    public async Task<List<Order>> Admin_GetOrders30Async()
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Include(oi=>oi.OrderItems).ThenInclude(p=>p.Product).OrderByDescending(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<List<Order>> Admin_GetOrdersByOrderIdAsync(int? orderId)
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Include(o=>o.OrderItems).ThenInclude(p=>p.Product).Where(i=>i.Id==orderId).ToListAsync();
        }
    }

    public async Task<int> Admin_TotalActiveOrdersCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Orders.Where(o =>o.Status==1).CountAsync();
        }
    }

    public async Task<Dictionary<int,int>> Admin_ShortOrdersByCategoryAsync()
    {
        using(var context = new DataContext())
        {
            var orders = await context.Orders.Include(oi=>oi.OrderItems).ThenInclude(p=>p.Product).Where(i=>i.Status==3).ToListAsync();
            
            var ordersByCategory = orders
            .SelectMany(o => o.OrderItems.Select(oi => oi.Product.CategoryId))
            .GroupBy(CategoryId => CategoryId)
            .ToDictionary(group => group.Key, group => group.Count());
            
            return ordersByCategory;
        }
    }
}