using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EnrollmentsController(AppDbContext db)
        {
            _db = db;
        }

        // Enroll a student in a course
        [HttpPost]
        public async Task<IActionResult> Enroll(Enrollment enrollment)
        {
            if (!await _db.Students.AnyAsync(s => s.Id == enrollment.StudentId))
                return BadRequest("Student not found");

            if (!await _db.Courses.AnyAsync(c => c.Id == enrollment.CourseId))
                return BadRequest("Course not found");

            _db.Enrollments.Add(enrollment);
            await _db.SaveChangesAsync();

            return Ok(enrollment);
        }

        // Get all courses a student is enrolled in
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var items = await _db.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .ToListAsync();

            return Ok(items);
        }

        // Get enrollment details by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var enrollment = await _db.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null) return NotFound();

            return Ok(enrollment);
        }
    }
}