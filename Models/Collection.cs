using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PA_project.Models
{
    public class Collection
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int collectionId;
        [Required]
        private int userId;
        [Required]
        private string title;
        [Required]
        private string description;
        [Required]
        private DateTime creationDate;

        private string iconUrl;

        public Collection() { }

        public int CollectionId { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public string IconUrl { get; set; }
        public virtual User USER { get; set; }
    }
}