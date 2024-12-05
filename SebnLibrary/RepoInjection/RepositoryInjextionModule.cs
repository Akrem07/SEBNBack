using Microsoft.Extensions.DependencyInjection;
using SEBNBack.Repositories;
using SebnLibrary.Abstract;
using SebnLibrary.Repo.Classes;
using SebnLibrary.Repo.Interfaces;

namespace SEBNLibrary.RepoInjection
{
    public static class RepositoryInjectionModule
    {

        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IRoleRepo, RoleRepo>();
            services.AddTransient<IFicheFonctionRepo, FicheFonctionRepo>();
            services.AddTransient<IIntegrationPlanRepo, IntegrationPlanRepo>();
            services.AddTransient<IDepartmentRepo, DepartmentRepo>();
            services.AddTransient<IExcelDataRepo, ExcelDataRepo>();
            
            services.AddTransient<IEmpEvaRepo, EmpEvaRepo>();
            services.AddTransient<IRespEvaRepo, RespEvaRepo>();


        }
    }
}
