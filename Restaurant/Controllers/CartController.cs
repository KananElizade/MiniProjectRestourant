
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restourant.Models;
using Restourant.Models;

namespace Restourant.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        var cart = CookieCartHelper.GetCart(HttpContext);
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int id, string name, string imgUrl, decimal price)
    {
        var cart = CookieCartHelper.GetCart(HttpContext);
        var item = cart.FirstOrDefault(x => x.ProductId == id);

        if (item != null)
        {
            item.Quantity++;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = id,
                Name = name,
                ImgUrl = imgUrl,
                Price = price,
                Quantity = 1
            });
        }

        CookieCartHelper.SaveCart(HttpContext, cart);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int id, int quantity)
    {
        var cart = CookieCartHelper.GetCart(HttpContext);
        var item = cart.FirstOrDefault(x => x.ProductId == id);

        if (item != null)
        {
            item.Quantity = quantity;
            if (item.Quantity <= 0)
                cart.Remove(item);
        }

        CookieCartHelper.SaveCart(HttpContext, cart);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(int id)
    {
        var cart = CookieCartHelper.GetCart(HttpContext);
        var item = cart.FirstOrDefault(x => x.ProductId == id);

        if (item != null)
            cart.Remove(item);

        CookieCartHelper.SaveCart(HttpContext, cart);
        return RedirectToAction("Index");
    }
}



public static class CookieCartHelper
{
    private const string CartCookieKey = "Cart";

    public static List<CartItem> GetCart(HttpContext context)
    {
        var cookie = context.Request.Cookies[CartCookieKey];
        return string.IsNullOrEmpty(cookie)
            ? new List<CartItem>()
            : JsonConvert.DeserializeObject<List<CartItem>>(cookie);
    }

    public static void SaveCart(HttpContext context, List<CartItem> cart)
    {
        var options = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            HttpOnly = true
        };
        string json = JsonConvert.SerializeObject(cart);
        context.Response.Cookies.Append(CartCookieKey, json, options);
    }

    public static void ClearCart(HttpContext context)
    {
        context.Response.Cookies.Delete(CartCookieKey);
    }
}
