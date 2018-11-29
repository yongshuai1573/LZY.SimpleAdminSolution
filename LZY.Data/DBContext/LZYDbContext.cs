﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Data.Common;
using LZY.Code;

namespace LZY.Data
{
    public partial class LZYDbContext : DbContext
    {
        public LZYDbContext()
            : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["LZYDbContext"],options=>options.UseRowNumberForPaging());
        }

        // public DbSet<UserEntity> UserEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string currentAssembleFileName = Assembly.GetExecutingAssembly().CodeBase.ToString();
            //Console.WriteLine("currentAssembleFileName:" + currentAssembleFileName);
            string assembleFileName = currentAssembleFileName.Replace(".Data.", ".Mapping.").Replace("file:///","");

            //Console.WriteLine(" pre assembleFileName Path: " + assembleFileName);

            if (assembleFileName.IndexOf(":") == -1)
                assembleFileName = @"/" + assembleFileName;
            //Console.WriteLine("assembleFileName Path: " + assembleFileName);
            Assembly asm = Assembly.LoadFile(assembleFileName);
            var typesToRegister = asm.GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                object configurationInstance = Activator.CreateInstance(type);
               
                modelBuilder.AddConfiguration(type, configurationInstance);
            }
            //modelBuilder.AddConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
