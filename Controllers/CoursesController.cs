using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CoursesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.Courses.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Courses.Add(course);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Course course)
        {
            if (id != course.Id)
                return BadRequest();

            _db.Entry(course).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Courses.Any(x => x.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}