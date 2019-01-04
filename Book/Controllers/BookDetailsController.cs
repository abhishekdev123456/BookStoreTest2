using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Book;
using Book.Models;

namespace Book.Controllers
{
    public class BookDetailsController : ApiController
    {
        private BooksDbContext db = new BooksDbContext();

        // GET: api/BookDetails
        [HttpGet]
        public List<BookModal> GetBookDetail()
        {
            BookDetail bd = new BookDetail();
            var books = db.BookDetail.ToList();
            List<BookModal> booklist = new List<BookModal>();
            foreach (var book in books)
            {
                BookModal bm = new BookModal();
                if (String.IsNullOrEmpty(book.authors))
                {
                    bm.authors = null;
                }
                else
                {
                    bm.authors = book.authors.Split(',');
                }
                bm.createDate = book.createDate;
                bm.dateOfPublication = book.dateOfPublication;
                bm.id = book.id;
                bm.name = book.name;
                bm.numberOfPages = book.numberOfPages;
                bm.updateDate = book.updateDate;
                booklist.Add(bm);
            }
            return booklist;
        }

        // GET: api/BookDetails/5
        [ResponseType(typeof(BookDetail))]
        public async Task<IHttpActionResult> GetBookDetail(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            BookDetail bookDetail = await db.BookDetail.FindAsync(id);
            if (bookDetail == null)
            {
                return NotFound();
            }
            BookModal bm = new BookModal();
            bm.id = bookDetail.id;
            bm.name = bookDetail.name;
            bm.createDate = bookDetail.createDate;
            bm.numberOfPages = bookDetail.numberOfPages;
            bm.dateOfPublication = bookDetail.dateOfPublication;
            bm.updateDate = bookDetail.updateDate;
            if (!String.IsNullOrEmpty(bookDetail.authors))
            {
                bm.authors = bookDetail.authors.Split(',');
            }

            return Ok(bm);
        }

        // PUT: api/BookDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBookDetail(BookModal saveBooks, string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var bookDetail = await db.BookDetail.Where(x => x.id == id).FirstOrDefaultAsync();

            if (bookDetail == null)
            {
                return BadRequest();
            }
            if (!String.IsNullOrEmpty(saveBooks.name))
            {
                bookDetail.name = saveBooks.name;
            }
            if (saveBooks.authors.Length > 0)
            {
                bookDetail.authors = string.Join(",", saveBooks.authors);
            }
            if (saveBooks.numberOfPages != null)
            {
                bookDetail.numberOfPages = saveBooks.numberOfPages;
            }
            if (saveBooks.createDate != null)
            {
                bookDetail.createDate = saveBooks.createDate;
            }
            if (saveBooks.updateDate != null)
            {
                bookDetail.updateDate = saveBooks.updateDate;
            }
            if (saveBooks.dateOfPublication != null)
            {
                bookDetail.dateOfPublication = saveBooks.dateOfPublication;
            }

            db.Entry(bookDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookDetailExists(bookDetail.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Sucessfully");
        }

        // POST: api/BookDetails
        [ResponseType(typeof(BookDetail))]
        public async Task<IHttpActionResult> PostBookDetail(BookModal saveBooks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BookDetail bookDetail = new BookDetail();
            bookDetail.id = Guid.NewGuid().ToString();
            bookDetail.name = saveBooks.name;
            bookDetail.numberOfPages = saveBooks.numberOfPages;
            if (saveBooks.authors.Length > 0)
            {
                bookDetail.authors = string.Join(",", saveBooks.authors);
            }
            bookDetail.createDate = saveBooks.createDate;
            bookDetail.updateDate = saveBooks.updateDate;
            bookDetail.dateOfPublication = saveBooks.dateOfPublication;
            db.BookDetail.Add(bookDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookDetailExists(bookDetail.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bookDetail.id }, bookDetail);
        }

        // DELETE: api/BookDetails/5
        [ResponseType(typeof(BookDetail))]
        public async Task<IHttpActionResult> DeleteBookDetail(string id)
        {
            BookDetail bookDetail = await db.BookDetail.FindAsync(id);
            if (bookDetail == null)
            {
                return NotFound();
            }

            db.BookDetail.Remove(bookDetail);
            await db.SaveChangesAsync();

            return Ok("Deleted Sucessfully");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookDetailExists(string id)
        {
            return db.BookDetail.Count(e => e.id == id) > 0;
        }
    }
}