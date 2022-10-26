﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly ILogger _logger;

    public BasketsController(IBasketRepository basketRepository, ILogger logger)
    {
        _basketRepository = basketRepository;
        _logger = logger;
    }

    [HttpGet("{username}", Name = "GetBasket")]
    [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Cart>> GetBasket([Required] string username)
    {
        _logger.Information($"BEGIN: GetBasketByUserName {username}");
        var result = await _basketRepository.GetBasketByUserName(username);
        _logger.Information($"END: GetBasketByUserName {username}");

        return Ok(result ?? new Cart(username));
    }

    [HttpPost(Name = "UpdateBasket")]
    [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Cart>> UpdateBasket([FromBody] Cart cart)
    {
        _logger.Information($"BEGIN: UpdateBasket for {cart.Username}");
        var options = new DistributedCacheEntryOptions()
            //set the absolute expiration time.
            .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(10))
            //a cached object will be expired if it not being requested for a defined amount of time period.
            //Sliding Expiration should always be set lower than the absolute expiration.
            .SetSlidingExpiration(TimeSpan.FromMinutes(10));

        var result = await _basketRepository.UpdateBasket(cart, options);
        _logger.Information($"END: UpdateBasket for {cart.Username}");
        return Ok(result);
    }

    [HttpDelete("{username}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteBasket([Required] string username)
    {
        _logger.Information($"BEGIN: DeleteBasket {username}");
        var result = await _basketRepository.DeleteBasketFromUserName(username);
        _logger.Information($"END: DeleteBasket {username}");
        return Ok(result);
    }
}