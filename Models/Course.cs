using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}