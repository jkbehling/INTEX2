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

            //Secure connection to aws env variables
            services.AddDbContext<CrashDbContext>(options =>
            {
                options.UseMySql(Environment.GetEnvironmentVariable("CrashDbConnection"));
            });

            //Secure connection to aws env variables
            services.AddDbContext<AppIdentityDBContext>(options =>
                options.UseMySql(Environment.GetEnvironmentVariable("IdentityConnection")));

            // this is the identity services for allowing people different roles and stuff
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddScoped<ICrashRepository, EFCrashRepository>();

            services.AddRazorPages();

            services.AddSession();

            services.AddServerSideBlazor();

            // this is code that we will use to implement 2FA through email
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

            // here is stuff for authorizing admin
            app.UseAuthentication();
            app.UseAuthorization();

            // allows us to use cookies that are displayed
            app.UseCookiePolicy();

            // this line is important in enabling HSTS
            app.UseHsts();

            // app.UseHttpsRedirection();


            // this is for the content security policy header
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy-Report-Only",
                    "default-src 'self'; report-uri /cspreport");
                await next();
            });


            // end points help with specifying what the url will show when directing routes
            app.UseEndpoints(endpoints =>
            {

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


            // this is called since we need to populate an admin user into database
            IdentitySeedData.EnsurePopulated(app);

        }
    }
}
