using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.APIs.Controllers;

//baskets
public class BasketController : BaseApiController
    {
    private readonly IBasketRespository _basketRespository;

    public BasketController(IBasketRespository basketRespository)
            {
        this._basketRespository = basketRespository;
    }

    [HttpGet] // GET: /api/Baskets?id=
    public async Task<ActionResult<CustmoerBasket>> GetBasket(string id)
    {
        var basket = await _basketRespository.GetBasketAsync(id);
        if (basket is null) return Ok(new CustmoerBasket(id));
        return Ok(basket);
    }


    [HttpPost] //POST: /api/Baskets
    public async Task<ActionResult<CustmoerBasket>> UpdateCustomerBasket(CustmoerBasket model)
    {
        var updatedOrAddedBasket = await _basketRespository.UpdateOrAddBasketAsync(model);
        if (updatedOrAddedBasket is null) return BadRequest(new ApiResponse(400));

        return Ok(updatedOrAddedBasket);

    }

    
    [HttpDelete] //DELETE:  /api/Baskets
    public async Task<ActionResult<bool>> DeleteBasket (string id)
    {
        return await _basketRespository.DeleteBasketAsync(id);

    }


    }

