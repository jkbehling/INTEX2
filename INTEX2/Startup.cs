using INTEX2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace INTEX2
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //THIS IS FOR THE CONTENT SECURITY POLICY HEADER
            services.AddControllersWithViews().AddMvcOptions(options =>
            {
                options.InputFormatters.OfType<SystemTextJsonInputFormatter>().First().SupportedMediaTypes.Add(
                    new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/csp-report")
                );
            });




            services.AddControllersWithViews();

            // used for .onnx file
            services.AddSingleton<InferenceSession>(
                new InferenceSession("wwwroot/crash_model (1).onnx")
);

            // add connection to mysql here
            services.AddDbContext<CrashDbContext>(options =>
            {
                options.UseMySql(Environment.GetEnvironmentVariable("CrashDbConnection"));
            });

            services.AddDbContext<AppIdentityDBContext>(options =>
                options.UseMySql(Environment.GetEnvironmentVariable("IdentityConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddScoped<ICrashRepository, EFCrashRepository>();

            services.AddRazorPages();

            services.AddSession();

            services.AddServerSideBlazor();

            //services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            //services.AddSingleton<IEmailSender, EmailSender>();

            // add services here for login, session, repository stuff
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            // app.UseMvc();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseHsts();
            // app.UseHttpsRedirection();

            // not sure if we will need this line ^


            //THIS IS FOR THE CONTENT SECURITY POLICY HEADER
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy-Report-Only",
                    "default-src 'self'; report-uri /cspreport");
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "county",
                //    pattern: "",
                //    default: );

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
                
            });

            IdentitySeedData.EnsurePopulated(app);

        }
    }
}
