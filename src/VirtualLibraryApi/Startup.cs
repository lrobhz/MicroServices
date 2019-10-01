using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using src.Models;
using Swashbuckle.AspNetCore.Swagger;
using VirtualLibraryApi.Models;
using VirtualLibraryApi.Auth;
using VirtualLibraryApi.Payment;

namespace VirtualLibraryApi
{
    public class Startup
    {
        public static List<Book> Books;
        public static List<BookReview> BookReviews = new List<BookReview>();
        public static Dictionary<int, Basket> Baskets = new Dictionary<int, Basket>();
        public static List<Order> Orders = new List<Order>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var random = new Random();
            Books = new List<Book>() 
            { 
                new Book() { BookId = 1, BookName = "Book1", Author = "Author1", Description = "Lorem ipsum dolor sit amet, consectetur ", Value = (float)(random.NextDouble() * 100)},
                new Book() { BookId = 2, BookName = "Book2", Author = "Author2", Description = "Lorem ipsum dolor sit amet, consectetur ", Value = (float)(random.NextDouble() * 100) },
                new Book() { BookId = 3, BookName = "Book3", Author = "Author3", Description = "Lorem ipsum dolor sit amet, consectetur ", Value = (float)(random.NextDouble() * 100) },
                new Book() { BookId = 4, BookName = "Book4", Author = "Author4", Description = "Lorem ipsum dolor sit amet, consectetur ", Value = (float)(random.NextDouble() * 100) },
                new Book() { BookId = 5, BookName = "Book5", Author = "Author5", Description = "Lorem ipsum dolor sit amet, consectetur ", Value = (float)(random.NextDouble() * 100) }
            };
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddScoped<AuthService>();
            services.AddScoped<CreditCardService>();
            services.AddScoped<TransactionLogService>();            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Virtual Library API", Version = "v1" });
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Virtual Library API V1");
            });
        }
    }
}
