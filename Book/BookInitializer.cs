using Book.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Book
{
    public class BookInitializer : DropCreateDatabaseIfModelChanges<BooksDbContext>
    {
        protected override void Seed(BooksDbContext context)
        {           
            var books = new List<BookDetail>
         {
       new BookDetail {
           id=Guid.NewGuid().ToString(),
           name="Java",
           authors = "william,Ram",
           numberOfPages = 100,
           createDate=1546535642,
           dateOfPublication=1446535642,
           updateDate=1546535642

       },
       new BookDetail {
           id=Guid.NewGuid().ToString(),
           name="SQL",
           authors = "Daniel,Munshi",
           numberOfPages = 500,
           createDate=1546535642,
           dateOfPublication=1446535642,
           updateDate=1546535642
       },
      
     };

            books.ForEach(b => context.BookDetail.Add(b));

            context.SaveChanges();
        }
    }
}