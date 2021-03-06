﻿
using ACME.Backend.Core.Data;
using ACME.Backend.Core.Data.Repository;
using ACME.Backend.Core.Data.Seed;
using ACME.Backend.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ACME.Backend.API.Helper
{
    public static class ServiceExtensions
    {
        public static void ConfigureSQLiteDBContext
            (
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            var sqliteConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RepositoryDBContext>(o => o.UseSqlite(sqliteConnectionString));
        }

        public static void ConfigureRepositoryWrapper
            (
                this IServiceCollection services
            )
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigurSeedDataIfNotExists
            (
                this IServiceCollection services
            )
        {
            services.AddTransient<Seed>();
        }

        public static void ConfigureAddOnServices
            (
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddControllers().AddNewtonsoftJson(opt => {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(
                                    configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //services.AddControllers(config =>
            //{
            //    config.RespectBrowserAcceptHeader = true;
            //    config.ReturnHttpNotAcceptable = true;

            //    //MvcOptions mvcOptions = new MvcOptions();
            //    //config.InputFormatters.Add(new XmlSerializerInputFormatter());
            //    config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            //}).AddXmlSerializerFormatters().AddXmlDataContractSerializerFormatters();
        }
    }
}
