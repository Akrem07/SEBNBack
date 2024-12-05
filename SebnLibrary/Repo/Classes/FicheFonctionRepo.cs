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
    public class FicheFonctionRepo : IFicheFonctionRepo
    {

        private readonly SEBNDbLibDbContext _context;
        IRepository<FicheFonction> _ficheFonction;

        public FicheFonctionRepo(SEBNDbLibDbContext context, IRepository<FicheFonction> ficheFonction)
        {
            _context = context;
            _ficheFonction = ficheFonction;
        }
        public async Task<bool> AddFFAsync(FicheFonction ficheFonction)
        {
            try
            {
                await _context.Set<FicheFonction>().AddAsync(ficheFonction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFFAsync(int id)
        {
            try
            {
                var ficheFonction = await _context.Set<FicheFonction>().FindAsync(id);
                if (ficheFonction == null)
                {
                    return false;
                }

                _context.Set<FicheFonction>().Remove(ficheFonction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<FicheFonction>> GetAll()
        {
            //try
            //{
            //    return await _context.Set<FicheFonction>().ToListAsync();
            //}
            //catch (Exception)
            //{
            //    // Handle or log the exception as needed
            //    throw;
            //}
            var listFF = await _context.FicheFonctions
                .ToListAsync();
            return listFF;
        }

        public async Task<FicheFonction> GetById(int id)
        {
            try
            {
                return await _context.Set<FicheFonction>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateFFAsync(int id, FicheFonction updatedFF)
        {
            try
            {
                var existingFF = await _context.Set<FicheFonction>().FindAsync(id);
                if (existingFF == null)
                {
                    return false;
                }


                existingFF.NameFf = updatedFF.NameFf;

                _context.Set<FicheFonction>().Update(existingFF);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
