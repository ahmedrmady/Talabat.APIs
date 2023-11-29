using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repositry.Data;

namespace Talabat.APIs.Controllers
{
  
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet ("NotFound")]
        public ActionResult NotFoundRequest()
        {
            return NotFound(new ApiResponse(404));

        }

        [HttpGet("serverError")]
        public ActionResult serverError()
        {

            var proudct = _dbContext.Products.Find(100);
            var value = proudct.ToString();
            return Ok();

        }


        [HttpGet("badrequest")]
        public ActionResult BadRequestE()
        {

            return BadRequest(new ApiResponse(400));

        }

        [HttpGet("badrequest/{id}")]
        public ActionResult BadRequestE(int id)
        {

            return BadRequest();

        }




    }
}
