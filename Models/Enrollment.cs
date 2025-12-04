using System;
using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        // Navigation properties (optional for POST)
        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}