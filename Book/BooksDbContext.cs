using Book.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Book
{
    public class BooksDbContext:DbContext
    {
        public BooksDbContext()
            :base("cn")
        {

        }

        public DbSet<BookDetail> BookDetail { get; set; }
    }
}