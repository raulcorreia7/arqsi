using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Closify.Models;
using Closify.Data;
using AutoMapper;
using Closify.Repository;
using Closify.Repository.Impl;
using Closify.Repository.Interfaces;
using Closify.Models.Servico;
//using AspNetCore.Firebase.Authentication.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Closify
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            /* Automapper configs
             */
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            /*
               Other configs
             */

            JwtBearerExtensions.AddJwtBearer(services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
, "Firebase", options =>
                             {
                                 //options.RequireHttpsMetadata = false;
                                 options.IncludeErrorDetails = true;
                                 //options.Authority = "https://securetoken.google.com/closify-ff3ee";
                                 //options.Audience = "closify-ff3ee";
                                 const string V = "yizbDrTxpEgaFO3xhN3mkIEUexb6c7UmldjPweak";
                                 options.TokenValidationParameters = new TokenValidationParameters
                                 {
                                     ValidateIssuer = true, // Issuer invalido
                                     ValidIssuer = "https://securetoken.google.com/closify-ff3ee",
                                     ValidateAudience = true,   // Audience invalido
                                     ValidAudience = "closify-ff3ee",
                                     ValidateLifetime = true,
                                     ValidateIssuerSigningKey = true,
                                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(V))
                                 };
                             });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddAuthenticationSchemes("Firebase").Build();
            });

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IMaterialAcabamentoRepository, MaterialAcabamentoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IAgregacaoRepository, AgregacaoRepository>();
            services.AddScoped<IDebugRepository, DebugRepository>();
            services.AddScoped<IAcabamentoRepository, AcabamentoRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IProdutoServico, ProdutoServico>();
            services.AddScoped<IValidarServico, ValidarServico>();

            // services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            // {
            //     option.Password.RequireDigit = false;
            //     option.Password.RequiredLength = 3;
            //     option.Password.RequiredUniqueChars = 0;
            //     option.Password.RequireLowercase = false;
            //     option.Password.RequireNonAlphanumeric = false;
            //     option.Password.RequireUppercase = false;
            // }).AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
            //services.AddFirebaseAuthentication(Configuration["FirebaseAuthentication:https://securetoken.google.com/closify-ff3ee"], Configuration["FirebaseAuthentication:closify-ff3ee"]);


            var contextName = "ClosifyDatabase";
            //services.AddDbContext<ClosifyContext>(options => options.UseSqlServer(Configuration.GetConnectionString(contextName)));

            services.AddDbContext<ClosifyContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString(contextName)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
