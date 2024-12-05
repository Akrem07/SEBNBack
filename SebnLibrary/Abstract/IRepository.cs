using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Abstract
{
    public interface IRepository<T> where T : class
    {

        Task<T> CreateAsync(T entity); //for creating 

        Task<IEnumerable<T>> FindAllAsync(); //List of  

        Task DeleteAsync(int id);
        Task DeleteAsync(string name);

        Task<bool> ExistsAsync(int id); // Check if an entity exists by ID

        Task<T> GetByIdAsync(int id); //get by id
        Task<List<T>> GetByNameAsync(string name);
        Task<List<T>> GetByLastNameAsync(string name);

        Task<T> UpdateAsync(T entity); //update entity
        Task AddAsync(T entity);
        object Include(Func<object, object> value);

    }
}
