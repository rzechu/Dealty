using Microsoft.AspNetCore.Mvc;
using Dealty.WebApi.Interfaces;
using Dealty.Shared.Filters;
using Dealty.Shared.Data;

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
        //[HttpGet("page")]
        //public async Task<ActionResult<Promotion>> Page(int pageNumber, int pageSize)
        //{
        //    var filter = new PaginationFilter(pageNumber, pageSize);
        //    var dbRecord = await _promotionRepositoryAsync.GetAllPaginatedAsync(filter.PageSize, filter.Offset);
        //    if (dbRecord == null)
        //    {
        //        _logger.Warn($"not found {pageNumber}");
        //        return NotFound();
        //    }
        //    return Ok(dbRecord);
        //}

        [HttpGet("page")]
        public async Task<ActionResult<ResponsePage<WebApi.Data.Promotion>>> Page(int pageNumber, int pageSize)
        {
            var paginationFilter = new PaginationFilter(pageNumber, pageSize);
            (IEnumerable<WebApi.Data.Promotion> dbRecords, int totalCount) result = await _promotionRepositoryAsync.GetAllPaginatedAsync(paginationFilter);
            var pagedList = new ResponsePage<WebApi.Data.Promotion>(result.dbRecords, paginationFilter, result.totalCount);

            if (result.dbRecords == null)
            {
                _logger.Warn($"not found {pageNumber}");
                return NotFound();
            }
            return Ok(pagedList);
        }


        // GET: api/Promotions
        [HttpGet]
        [AuthorizeDealty]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<WebApi.Data.Promotion>>> GetPromotions()
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
        public async Task<ActionResult<WebApi.Data.Promotion>> GetPromotion(int id)
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
        [AuthorizeDealty]
        public async Task<IActionResult> PutPromotion(int id, WebApi.Data.Promotion promotion)
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
               [AuthorizeDealty]
        public async Task<ActionResult<WebApi.Data.Promotion>> PostPromotion(WebApi.Data.Promotion promotion)
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
        [AuthorizeDealty]
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