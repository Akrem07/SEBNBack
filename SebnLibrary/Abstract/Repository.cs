using Microsoft.EntityFrameworkCore;
using SebnLibrary.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Abstract
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SEBNDbLibDbContext _context;

        public Repository(SEBNDbLibDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException("Entity not found");
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity != null;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public object Include(Func<object, object> value)
        {
            throw new NotImplementedException();
        }



        public async Task DeleteAsync(string name)
        {
            T entity = await _context.Set<T>().FindAsync(name);

            if (entity == null)
            {
                throw new ArgumentException("Entity not found");
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetByNameAsync(string name)
        {
            return await _context.Set<T>()
                .Where(entity => EF.Property<string>(entity, "FirstName") == name)
                .ToListAsync();
        }
        public async Task<List<T>> GetByLastNameAsync(string name)
        {
            return await _context.Set<T>()
                .Where(entity => EF.Property<string>(entity, "LastName") == name)
                .ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
