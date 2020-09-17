using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookList.Data;
using BookList.Models;

namespace BooklistBackend.Controllers
{
    [Route("booklist")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookContext _context;

        public BooksController(BookContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            return await _context.Book.Include(s => s.comments).ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            //var book = await _context.Book.FindAsync(id);
            var book = await _context.Book.Include(s => s.comments).FirstOrDefaultAsync(i => i.bookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.bookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw(e);
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.bookId }, book);
        }

        
        // PUT
        //[HttpPost("{bookId}/{commentId}/UpdateBookComments")]
        //public async Task<ActionResult> UpdateCommentsForBooks(int bookId,int commentId, BookComments updateBookComments)
        //{
        //    if (bookId != updateBookComments.bookId || commentId!= updateBookComments.commentId)
        //    {
        //        return BadRequest();
        //    }
        //    var existingBook = _context.Book.Where(s => s.bookId == bookId).Include(s => s.comments).SingleOrDefault();
        //    if (existingBook != null)
        //    {
        //        var existingBookComment = existingBook.comments.Where(add => add.commentId == updateBookComments.commentId).SingleOrDefault();
        //        if(existingBookComment != null)
        //        {
        //            _context.Entry(existingBookComment).CurrentValues.SetValues(updateBookComments);
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    else
        //    {
        //        return NotFound("Book ID does not exist");
        //    }
        //    return Ok();
        //}

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.bookId == id);
        }
    }
}
