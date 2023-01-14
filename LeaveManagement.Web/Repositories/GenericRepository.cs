using LeaveManagement.Web.Contracts;
using LeaveManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        //constructor
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
            


        public async Task<T> AddAsync(T entity)
        {            
            
               await context.AddAsync(entity);
               await context.SaveChangesAsync();
               return entity; //we are just returning back the data we added, might be a situation this is needed; entity will actually have a
                //db id post SaveChanges; i.e. the entity ID will be updated and this is an efficeint way to return the id
            
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);//using the method implemented below          
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id); //using the method implemented below
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int? id)
        {
            if(id == null)
            {
                return null;
            }
            else
            {
                return await context.Set<T>().FindAsync(id);
            }
            
        }

        public async Task UpdateAsync(T entity)
        {
            //this means: take the original data of the entity, just mark its state as "modified" (then in the override of the save changes will 
            //catch the change of the base DateCreated and DateModified
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
