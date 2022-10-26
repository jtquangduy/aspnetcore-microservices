﻿namespace Basket.API.Entities;

public class Cart
{
    public Cart()
    {
    }

    public Cart(string username)
    {
        Username = username;
    }

    public string Username { get; set; }

    public List<CartItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(item => item.ItemPrice * item.Quantity);
}