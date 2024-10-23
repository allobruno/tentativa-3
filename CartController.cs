using Microsoft.AspNetCore.Mvc;
using WebCafe.Models;
using WebCafe.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace WebCafe.Controllers
{
    public class CartController : Controller
    {
        // Adiciona itens ao carrinho
        public IActionResult AddToCart(int id, string name, decimal price, string imageUrl, string variant)
        {
            // Obtém o carrinho da sessão ou cria uma nova lista se não existir
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Cria um identificador único combinando o ID do produto com sua variante
            string uniqueId = id + "_" + variant;

            // Verifica se o item já está no carrinho com base no identificador único
            var itemInCart = cart.FirstOrDefault(i => i.UniqueId == uniqueId);

            if (itemInCart == null)
            {
                // Se o item não estiver no carrinho, adiciona um novo item com o identificador único
                cart.Add(new CartItem { UniqueId = uniqueId, Id = id, Name = name, Price = price, Quantity = 1, ImageUrl = imageUrl, Variant = variant });
            }
            else
            {
                // O item já está no carrinho, apenas aumenta a quantidade
                itemInCart.Quantity++;
            }

            // Atualiza o carrinho na sessão
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            // Redireciona para a tela do carrinho
            return RedirectToAction("Carrinho");
        }


        // Exibe o carrinho
        public IActionResult Carrinho()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        // Remove um item do carrinho
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cart != null)
            {
                CartItem itemToRemove = cart.FirstOrDefault(i => i.Id == id);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            return RedirectToAction("Carrinho");
        }

        // Atualiza a quantidade de um item
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cart != null)
            {
                CartItem item = cart.FirstOrDefault(i => i.Id == id);
                if (item != null && quantity > 0)
                {
                    item.Quantity = quantity;
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            return RedirectToAction("Carrinho");
        }
        [HttpPost]
        public IActionResult IncreaseQuantity(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            var item = cart.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                item.Quantity++;
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Carrinho");
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            var item = cart.FirstOrDefault(i => i.Id == id);
            if (item != null && item.Quantity > 1) // Impede de diminuir abaixo de 1
            {
                item.Quantity--;
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Carrinho");
        }
        public List<CartItem> GetCartItems()
        {
            // Obtém os itens do carrinho da sessão ou retorna uma nova lista se estiver vazio
            return HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }
        public IActionResult Pagamento()
        {
            var carrinho = GetCartItems(); // Chama o método para obter os itens do carrinho
            var totalCarrinho = carrinho.Sum(item => item.Price * item.Quantity) + 50; // Soma o total com frete
            ViewBag.TotalCarrinho = totalCarrinho;

            return View(carrinho); // Passa o carrinho para a View
        }
        public IActionResult MeusPedidos()
        {
            // Simula uma lista de pedidos baseada no carrinho
            List<CartItem> carrinho = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var pedidos = new List<PedidoViewModel>
            {
                new PedidoViewModel
                {
                    NumeroPedido = 12537,
                    DataRealizacao = DateTime.Now, // Hoje como previsão
                    PrevisaoEntrega = DateTime.Now.AddDays(+8),  // Data de 8 dias adiante
                    ValorTotal = carrinho.Sum(item => item.Price * item.Quantity),
                    Itens = carrinho // Usa os itens do carrinho para simular os itens do pedido
                }
            };

            return View(pedidos);
        }

    }
}
