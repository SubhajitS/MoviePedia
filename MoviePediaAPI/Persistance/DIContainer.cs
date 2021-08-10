using Entities.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance
{
    public static class DIContainer
    {
        public static void RegisterDI(this IServiceCollection services)
        {
            services.AddScoped<IMoviesRepository, MoviesRepository>();
        }
    }
}
