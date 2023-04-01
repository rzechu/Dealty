using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dealty.WebApi.Data;
using Dealty.WebApi.Interfaces;
using NLog;

namespace Dealty.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly Logger.IDealtyLogger _logger;
        //private readonly DealtyDBContext _context;

        private readonly IPromotionRepositoryAsync _promotionRepositoryAsync;

        public PromotionsController(IPromotionRepositoryAsync promotionRepositoryAsync, Logger.IDealtyLogger logger)
        {
            //_context = context;
            _promotionRepositoryAsync = promotionRepositoryAsync;
            _logger = logger;
        }

        // GET: api/Promotions/page/5
        [HttpGet("page")]
        public async Task<ActionResult<Promotion>> Page(int page)
        {
            int size = 3;
            var dbRecord = await _promotionRepositoryAsync.GetAllPaginatedAsync(size, page == 1 ? 0 : size*(page - 1) );
            if (dbRecord == null)
            {
                _logger.Warn($"not found {page}");
                return NotFound();
            }
            return Ok(dbRecord);
        }


        // GET: api/Promotions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetPromotions()
        {
            var dbRecord = await _promotionRepositoryAsync.GetAllAsync();
            if (dbRecord == null)
            {
                _logger.Warn("not found");
                return NotFound();
            }
            return Ok(dbRecord);
        }

        // GET: api/Promotions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetPromotion(int id)
        {
            var dbRecord = await _promotionRepositoryAsync.GetByIdAsync(id);
            if (dbRecord == null)
            {
                _logger.Warn($"not found {id}");
                return NotFound();
            }
            return Ok(dbRecord);
        }

        // PUT: api/Promotions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromotion(int id, Promotion promotion)
        {
            if (id != promotion.PromotionID)
            {
                return BadRequest();
            }

            var dbRecord = await _promotionRepositoryAsync.UpdateAsync(promotion);

            if(dbRecord == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{nameof(promotion)} can't be updated");
            }

            return NoContent();
        }

        // POST: api/Promotions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Promotion>> PostPromotion(Promotion promotion)
        {
            var dbRecord = await _promotionRepositoryAsync.AddAsync(promotion);
            
          if (dbRecord == null)
          {
              return Problem("Entity set 'DealtyDBContext.Promotions'  is null.");
          }
            
            return CreatedAtAction("GetPromotion", new { id = promotion.PromotionID }, promotion);
        }

        // DELETE: api/Promotions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            var dbRecord = await _promotionRepositoryAsync.GetByIdAsync(id);
            (bool status, string message) = await _promotionRepositoryAsync.DeleteAsync(dbRecord);
            
            if (status == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
            return StatusCode(StatusCodes.Status200OK, dbRecord);
        }
    }
}