using CookBook.Domain.Repository;
using CookBook.Domain.Repository.User;
using CookBook.Infrastructure.DataAccess;
using CookBook.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddDbContext(services);
            AddRepositories(services);
        }

        private static void AddDbContext(IServiceCollection services)
        {
            var connectionString = "Server=localhost;Database=cookbookapi;Uid=root;Pwd=123456;";
            var serverVersion = new MySqlServerVersion(new Version(8,0,42));

            services.AddDbContext<CookBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
             
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
