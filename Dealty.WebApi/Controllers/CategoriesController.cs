using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dealty.WebApi.Data;
using Dealty.WebApi.Interfaces;

namespace Dealty.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        //private readonly DealtyDBContext _context;
        private readonly ICategoryRepositoryAsync _categoryRepositoryAsync;

        public CategoriesController(ICategoryRepositoryAsync categoryRepositoryAsync)
        {
            //_context = context;
            _categoryRepositoryAsync = categoryRepositoryAsync;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var dbRecord = await _categoryRepositoryAsync.GetAllAsync();
            if (dbRecord == null)
            {
                return NotFound();
            }
            return Ok(dbRecord);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var dbRecord = await _categoryRepositoryAsync.GetByIdAsync(id);
            if (dbRecord == null)
            {
                return NotFound();
            }
            return Ok(dbRecord);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            var dbRecord = await _categoryRepositoryAsync.UpdateAsync(category);

            if(dbRecord == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{nameof(category)} can't be updated");
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            var dbRecord = await _categoryRepositoryAsync.AddAsync(category);
            
          if (dbRecord == null)
          {
              return Problem("Entity set 'DealtyDBContext.Categories'  is null.");
          }
            
            return CreatedAtAction("GetCategory", new { id = category.CategoryID }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var dbRecord = await _categoryRepositoryAsync.GetByIdAsync(id);
            (bool status, string message) = await _categoryRepositoryAsync.DeleteAsync(dbRecord);
            
            if (status == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
            return StatusCode(StatusCodes.Status200OK, dbRecord);
        }
    }
}