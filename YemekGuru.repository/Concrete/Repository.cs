using Microsoft.EntityFrameworkCore;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class Repository<TEntity,TContext> : IRepository<TEntity>
where TContext :DbContext, new()
where TEntity :class
{
    public async Task<bool> CreateAsync(TEntity entity) //Oluşturma
    {
        try{
            using(var context = new TContext())
            {
                context.Set<TEntity>().Add(entity);
                await context.SaveChangesAsync();
            }
            return true;
        }
        catch(Exception err){
            Console.WriteLine(err);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(TEntity entity) //Silme
    {
        try{
            using(var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
            }
            return true;
        }
        catch(Exception err){
            Console.WriteLine(err);
            return false;
        }
    }

    public async Task<List<TEntity>> GetAllAsync() 
    {
        using(var context = new TContext())
        {
            return await context.Set<TEntity>().ToListAsync();
        }
    }

    public async Task<bool> UpdateAsync(TEntity entity) //Güncelleme
    {
        try{
            using(var context = new TContext())
            {
                context.Update(entity);
                await context.SaveChangesAsync();
            }
            return true;
        }
        catch(Exception err){
            Console.WriteLine(err);
            return false;
        }
    }
}