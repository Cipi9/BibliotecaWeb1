using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PA_project.Models
{
    public class User
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int userId;
        [Required(ErrorMessage = "Please enter your first name.")]
        private string firstName;
        [Required(ErrorMessage = "Please enter your last name.")]
        private string lastName;
        [Required(ErrorMessage = "Please enter your username.")]
        private string username;
        [Required(ErrorMessage = "Please enter an email.")]
        [DataType(DataType.EmailAddress)]
        private string email;
        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        private string password;

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User() { }
    }
}