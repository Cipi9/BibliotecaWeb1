using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PA_project.Models
{
    public class CollectionBooks
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int collectionBooksId;
        [Required]
        private int collectionId;
        [Required]
        private int booksId;

        public CollectionBooks() { }

        public int CollectionBooksId { get; set; }
        public int CollectionId { get; set; }

        public int BooksId { get; set; }
    }
}