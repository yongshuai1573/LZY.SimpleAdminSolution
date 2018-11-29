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


        }
    }
}
