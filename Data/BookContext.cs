using BookList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


namespace BookList.Data
{
    public class BookContext : DbContext
    {
        // an empty constructor
        public BookContext() { }

        // base(options) calls the base class's constructor,
        // in this case, our base class is DbContext
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        // Use DbSet<Book> to query or read and 
        // write information about A Book
        public DbSet<Book> Book { get; set; }
        public static System.Collections.Specialized.NameValueCollection AppSettings { get; }

        // configure the database to be used by this context
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            // booklistConnection is the name of the key that
            // contains the has the connection string as the value
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("booklistConnection"));
        }
    }
}
