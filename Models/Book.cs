
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Models
{
    public class Book
    {
        public Book()
        {
            comments = new List<BookComments>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bookId { get; set; }
        [Required]
        public string bookTitle { get; set; }
        [Required]
        public string bookAuthor { get; set; }
        public string bookImage { get; set; }
        public List<BookComments> comments { get;set;}
    }
}
