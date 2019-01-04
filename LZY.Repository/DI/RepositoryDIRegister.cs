using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LZY.Repository
{
    public class RepositoryDIRegister
    {
        public static void DIRegister(IServiceCollection services)
        {           
            services.AddTransient<It_userRepository, t_userRepository>();
            services.AddTransient<It_moduleRepository, t_moduleRepository>();
            services.AddTransient<It_paratypeRepository, t_paratypeRepository>();
            services.AddTransient<It_para_valuesRepository, t_para_valuesRepository>();



        }
    }
}
