using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Models
{
    public class BookComments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int commentId { get; set;}

        [ForeignKey("Book")]
        public int bookId { get; set; }
        public string comments { get; set; }
        public string user { get; set; }
    }
}
