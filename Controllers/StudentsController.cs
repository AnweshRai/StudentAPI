using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public StudentsController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/students
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _db.Students.ToListAsync();
            return Ok(students);
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return NotFound();

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest("ID mismatch");

            _db.Entry(student).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Students.Any(x => x.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return NotFound();

            _db.Students.Remove(student);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}