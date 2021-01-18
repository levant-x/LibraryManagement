using LibraryManagement.Data.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LibraryDbContext>();
                context.Customers.Add(new Customer
                {
                    Name = "Abdul Aziz"
                });
                context.Customers.Add(new Customer
                {
                    Name = "Osama ben Laden"
                });
                context.Customers.Add(new Customer
                {
                    Name = "Muhammed Ali"
                });
                context.Authors.Add(new Author
                {
                    Name = "M J DeMarco",
                    Books = new List<Book>()
                    {
                        new Book { Title = "The Millionaire Fastlane" },
                        new Book { Title = "Unscripted" }
                    }
                });
                context.Authors.Add(new Author
                {
                    Name = "Grant Cardone",
                    Books = new List<Book>()
                    {
                        new Book { Title = "The 10X Rule"},
                        new Book { Title = "If You're Not First, You're Last"},
                        new Book { Title = "Sell To Survive"}
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
