using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}