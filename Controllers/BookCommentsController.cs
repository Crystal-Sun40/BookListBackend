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
    [Route("bookComments")]
    [ApiController]
    public class BookCommentsController : ControllerBase
    {
        private readonly BookContext _context;

        public BookCommentsController(BookContext context)
        {
            _context = context;
        }

        // GET: api/BookComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookComments>>> GetBookComments()
        {
            return await _context.BookComments.ToListAsync();
        }

        // GET: api/BookComments/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<BookComments>> GetBookComments(int id)
        //{
        //    var bookComments = await _context.BookComments.FindAsync(id);

        //    if (bookComments == null)
        //    {
        //        return NotFound();
        //    }

        //    return bookComments;
        //}

        // PUT: api/BookComments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBookComments(int id, BookComments bookComments)
        //{
        //    if (id != bookComments.commentId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(bookComments).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookCommentsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/BookComments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BookComments>> PostBookComments(BookComments bookComments)
        {
            _context.BookComments.Add(bookComments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookComments", new { id = bookComments.commentId }, bookComments);
        }

        [HttpPost("{bookId}")]
        public async Task<ActionResult> AddCommentsForBooks(int bookId, BookComments newBookComments)
        {
            if (bookId != newBookComments.bookId)
            {
                return BadRequest();
            }
            var existingBook = _context.Book.Where(s => s.bookId == bookId).Include(s => s.comments).SingleOrDefault();
            if (existingBook != null)
            {
                existingBook.comments.Add(newBookComments);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound("Book ID does not exist");
            }
            return Ok();
        }


        // DELETE: api/BookComments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookComments>> DeleteBookComments(int id)
        {
            var bookComments = await _context.BookComments.FindAsync(id);
            if (bookComments == null)
            {
                return NotFound();
            }

            _context.BookComments.Remove(bookComments);
            await _context.SaveChangesAsync();

            return bookComments;
        }

        private bool BookCommentsExists(int id)
        {
            return _context.BookComments.Any(e => e.commentId == id);
        }
    }
}
