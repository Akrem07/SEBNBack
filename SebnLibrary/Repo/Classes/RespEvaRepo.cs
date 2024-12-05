using Microsoft.EntityFrameworkCore;
using SebnLibrary.Abstract;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Classes
{
    public class RespEvaRepo : IRespEvaRepo
    {
        private readonly SEBNDbLibDbContext _context;
        IRepository<RespEva> _respEva;
        public RespEvaRepo(SEBNDbLibDbContext context, IRepository<RespEva> respEva)
        {
            _context = context;
            _respEva = respEva;
        }

        public async Task<List<RespEva>> GetEvaluationsAsync()
        {
            try
            {
                return await _context.Set<RespEva>().ToListAsync();
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
                var evaluation = await _context.Set<RespEva>().FindAsync(id);
                if (evaluation == null)
                {
                    return false;
                }

                _context.Set<RespEva>().Remove(evaluation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<bool> AddEvaAsync(RespEva respEva)
        {
            try
            {

                await _respEva.CreateAsync(respEva);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<RespEva> GetByMat(int mat)
        {
            try
            {
                return await _context.Set<RespEva>().FindAsync(mat);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
