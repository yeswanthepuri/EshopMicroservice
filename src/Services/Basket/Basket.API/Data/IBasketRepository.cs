﻿using System.Threading;
using System.Threading.Tasks;
using Basket.API.Models;

namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken);
        Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken);
        Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken);

    }
}
