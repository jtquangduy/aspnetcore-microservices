using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly ILogger _logger;
    private readonly IDistributedCache _redisCacheService;
    private readonly ISerializeService _serializeService;

    public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, ILogger logger)
    {
        _redisCacheService = redisCacheService;
        _serializeService = serializeService;
        _logger = logger;
    }

    public async Task<Cart?> GetBasketByUserName(string userName)
    {
        _logger.Information($"Begin: GetBasketByUserName {userName}");
        var basket = await _redisCacheService.GetStringAsync(userName);
        _logger.Information($"End: GetBasketByUserName {userName}");

        return string.IsNullOrEmpty(basket) ? null : _serializeService.Deserialize<Cart>(basket);
    }

    public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
    {
        _logger.Information($"Begin: UpdateBasket {cart.Username}");
        if (options != null)
            await _redisCacheService.SetStringAsync(cart.Username,
                _serializeService.Serialize(cart), options);
        else
            await _redisCacheService.SetStringAsync(cart.Username,
                _serializeService.Serialize(cart));

        _logger.Information($"End: UpdateBasket {cart.Username}");

        return await GetBasketByUserName(cart.Username);
    }

    public async Task<bool> DeleteBasketFromUserName(string username)
    {
        try
        {
            _logger.Information($"BEGIN: DeleteBasketFromUserName {username}");
            await _redisCacheService.RemoveAsync(username);
            _logger.Information($"END: DeleteBasketFromUserName {username}");
            return true;
        }
        catch (Exception e)
        {
            _logger.Error("Error DeleteBasketFromUserName: " + e.Message);
            throw;
        }
    }
}