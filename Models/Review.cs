using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PA_project.Models
{
    public class Review
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int reviewId;
        [Required]
        private int userId;
        [Required]
        private int bookId;
        [Required]
        private string text;
        [Required]
        private DateTime creationDate;

        public Review() { }

        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User USER { get; set; }
        public virtual Book BOOK { get; set; }

    }
}