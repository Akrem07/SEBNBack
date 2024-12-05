using SEBNBack.Services;
using SEBNBack.Services.Classes;
using SEBNBack.Services.Interfaces;

namespace SEBNBack.ServiceInjection
{
    public static class ServiceInjectionModel
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IFicheFonctionService, FicheFonctionService>();
            services.AddTransient<IIntegrationPlanService, IntegrationPlanService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IExcelDataService, ExcelDataService>();

            services.AddTransient<IEmpEvaService, EmpEvaService>();
            services.AddTransient<IRespEvaService,RespEvaService>();

            return services;

        }

    }
}
