using SebnLibrary.Abstract;
using SebnLibrary.ModelEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using SebnLibrary.Repo.Interfaces;

namespace SEBNBack.Repositories
{
    public class EmpEvaRepo : IEmpEvaRepo
    {
        private readonly SEBNDbLibDbContext _context;
        IRepository<EmpEva> _empEva;
        public EmpEvaRepo(SEBNDbLibDbContext context, IRepository<EmpEva> empEva)
        {
            _context = context;
            _empEva = empEva;
        }

        public async Task<List<EmpEva>> GetEvaluationsAsync()
        {
            try
            {
                return await _context.Set<EmpEva>().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteEvaluationAsync(int id) 
        {
            try
            {
                var evaluation = await _context.Set<EmpEva>().FindAsync(id);
                if (evaluation == null)
                {
                    return false;
                }

                _context.Set<EmpEva>().Remove(evaluation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<bool> AddEvaAsync(EmpEva empEva)
        {
            try
            {

                await _empEva.CreateAsync(empEva);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<EmpEva> GetByMat(int mat)
        {
            try
            {
                return await _context.Set<EmpEva>().FindAsync(mat);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
