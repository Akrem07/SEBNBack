using Microsoft.EntityFrameworkCore;
using SebnLibrary.Abstract;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System;

namespace SebnLibrary.Repo.Classes
{
    public class IntegrationPlanRepo : IIntegrationPlanRepo
    {
        private readonly SEBNDbLibDbContext _context;
        IRepository<IntegrationPlan> _integrationPlan;

        public IntegrationPlanRepo(SEBNDbLibDbContext context, IRepository<IntegrationPlan> integrationPlan)
        {
            _context = context;
            _integrationPlan = integrationPlan;
        }

        public async Task<bool> AddIPAsync(IntegrationPlan integrationPlan)
        {
            try
            {
                await _context.Set<IntegrationPlan>().AddAsync(integrationPlan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle or log the exception as needed
                return false;
            }
        }

        public async Task<bool> DeleteIPAsync(int id)
        {
            try
            {
                var integrationPlan = await _context.Set<IntegrationPlan>().FindAsync(id);
                if (integrationPlan == null)
                {
                    return false;
                }

                _context.Set<IntegrationPlan>().Remove(integrationPlan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle or log the exception as needed
                return false;
            }
        }

        public async Task<List<IntegrationPlan>> GetAll()
        {
            var listIP = await _context.IntegrationPlans
                .ToListAsync();
            return listIP;

        }

        public async Task<IntegrationPlan> GetById(int id)
        {
            try
            {
                return await _context.Set<IntegrationPlan>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateIPAsync(int id, IntegrationPlan updatedIP)
        {
            try
            {
                var existingIP = await _context.Set<IntegrationPlan>().FindAsync(id);
                if (existingIP == null)
                {
                    return false;
                }

                // Update properties
                existingIP.NameIp = updatedIP.NameIp;

                _context.Set<IntegrationPlan>().Update(existingIP);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        public async Task<byte[]?> GetExcelFileDataByUserMat(int mat)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.IdIpNavigation)
                    .ThenInclude(ip => ip.FileData)  // Include the FileData in the result
                    .FirstOrDefaultAsync(u => u.Mat == mat);

                return user?.IdIpNavigation?.FileData;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error in GetExcelFileDataByUserMat: {ex.Message}");
                throw;
            }
        }


    }
}
