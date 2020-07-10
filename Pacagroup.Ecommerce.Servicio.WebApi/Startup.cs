using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Aplicacion.Interface;
using Pacagroup.Ecommerce.Aplicacion.Main;
using Pacagroup.Ecommerce.Dominio.Core;
using Pacagroup.Ecommerce.Dominio.Interface;
using Pacagroup.Ecommerce.Infraestructura.Data;
using Pacagroup.Ecommerce.Infraestructura.Interface;
using Pacagroup.Ecommerce.Infraestructura.Repository;
using Pacagroup.Ecommerce.Servicio.WebApi.Helpers;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Logging;
using Pacagroup.Ecommerce.Transversal.Mapper;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Servicio.WebApi
{
    /// <summary>
    /// Controller Customer.
    /// </summary>
    public class Startup
    {
        readonly string myPolicy = "policyApiEcommerce";

        /// <summary>
        /// Controller Customer.
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Controller Customer.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));
            services.AddCors(option => option.AddPolicy(myPolicy, builder => builder.WithOrigins(Configuration["Config:OriginCors"]).AllowAnyHeader().AllowAnyMethod()));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); });

            // Obtenemos una sección del archivo appsettings.json
            var appSettingsSection = Configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingsSection);

            // Configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();

            // Inyección de Dependencias entre las interfaces y sus implementaciones
            services.AddSingleton<IConfiguration>(Configuration); //Se instancia una sola vez y es reutilizado
            services.AddSingleton<IConnectionFactory, ConnectionFactory>(); //Se instancia una sola vez y es reutilizado
            services.AddScoped<ICustomerAplicacion, CustomerAplicacion>(); //Se instancia una vez por solicitud
            services.AddScoped<ICustomerDominio, CustomerDominio>(); //Se instancia una vez por solicitud
            services.AddScoped<ICustomerRepository, CustomerRepository>(); //Se instancia una vez por solicitud
            services.AddScoped<IUserAplicacion, UserAplicacion>(); //Se instancia una vez por solicitud
            services.AddScoped<IUserDominio, UserDominio>(); //Se instancia una vez por solicitud
            services.AddScoped<IUserRepository, UserRepository>(); ////Se instancia una vez por solicitud
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>)); ////Se instancia una vez por solicitud

            // Obtiene los valores de la configuración inyectada
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var Issuer = appSettings.Issuer;
            var Audience = appSettings.Audience;

            // Registramos la autenticacion / Aqui le decimos que sera Token Bearer
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = int.Parse(context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero //Diferencia en las horas
                };
            });

            // Registramos el generador Swagger, definimos 1 o más documentos Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Pacagroup Technology Services API Market",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Guillermo Alvarez",
                        Email = "galvarezp@outlook.com",
                        Url = "https://pacagroup.com"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                // 
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Indicamos a Swagger que los metodos de las APIs van a utilizar autenticación 
                c.AddSecurityDefinition("Authorization", new ApiKeyScheme
                {
                    Description = "Authorization by API key",
                    In = "header",
                    Type = "apiKey",
                    Name = "Authorization"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Authorization", new string[0]}
                });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Habilitamos en Middleware para servir Swagger-UI (HTML, JS, CSS, etc.)
            // Especificamos el endpoint Swagger JSON
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Ecommerce V1");
            });

            app.UseCors(myPolicy); //Usará Cors
            app.UseAuthentication(); //Usará Autenticación (JWT)
            app.UseMvc();
        }
    }
}
