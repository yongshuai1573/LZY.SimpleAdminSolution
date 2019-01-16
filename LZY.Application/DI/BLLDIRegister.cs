using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using LZY.Repository;
using LZY.Application;

namespace LZY.Application
{
    public class BLLDIRegister
    {
        public static void DIRegister(IServiceCollection services)
        {            //配置一个依赖注入映射关系        
            services.AddTransient<IUserBus,UserBus>();
            services.AddTransient<IParaBus, ParaBus>();
            services.AddTransient<IParaValueBus, ParaValueBus>();
            services.AddTransient<IModuleBus, ModuleBus>();

            //注册DAL层的依赖注入  
            RepositoryDIRegister.DIRegister(services);
        }
          
    }
}
