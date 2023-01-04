using Microsoft.AspNetCore.Mvc;
using Pedalea.WebApp.Models;
using System.Text.Json;
using static Pedalea.WebApp.Helpers.ModalHelper;

namespace Pedalea.WebApp.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public PedidosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Pedido> pedidos = new List<Pedido>();
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("PedaleaApiPedidos");
                HttpResponseMessage response = await client.GetAsync("api/pedidos/GetPedidos");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        pedidos = JsonSerializer.Deserialize<IEnumerable<Pedido>>(content, options);
                    }
                    return View(pedidos);
                }
                return View();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            Pedido pedido = new();
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpClient client = _httpClientFactory.CreateClient("PedaleaApiPedidos");
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/pedidos/AddPedido", pedido);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {
                            Pedido result = JsonSerializer.Deserialize<Pedido>(content, options);
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(pedido);
        }


        [NoDirectAccess]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("PedaleaApiPedidos");
                HttpResponseMessage response = await client.DeleteAsync($"api/pedidos/DeletePedido/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pedido pedido = new();
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("PedaleaApiPedidos");
                HttpResponseMessage response = await client.GetAsync($"api/pedidos/GetPedido/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        pedido = JsonSerializer.Deserialize<Pedido>(content, options);
                    }
                    return View(pedido);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NotFound();
        }
    }
}
