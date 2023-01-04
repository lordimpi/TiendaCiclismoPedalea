using Microsoft.AspNetCore.Mvc;
using Pedalea.WebApp.Models;
using System.Text.Json;
using static Pedalea.WebApp.Helpers.ModalHelper;

namespace Pedalea.WebApp.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public ProductosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Producto> productos = new List<Producto>();
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("PedaleaApiProductos");
                HttpResponseMessage response = await client.GetAsync("api/productos/GetProducts");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        productos = JsonSerializer.Deserialize<IEnumerable<Producto>>(content, options);
                    }
                    return View(productos);
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
            Producto producto = new();
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpClient client = _httpClientFactory.CreateClient("PedaleaApiProductos");
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/productos/AddProduct", producto);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {
                            Producto result = JsonSerializer.Deserialize<Producto>(content, options);
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
            return View(producto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Producto producto = new();
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("PedaleaApiProductos");
                HttpResponseMessage response = await client.GetAsync($"api/productos/GetProduct/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        producto = JsonSerializer.Deserialize<Producto>(content, options);
                    }
                    return View(producto);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Producto model)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Producto producto = new();
                    HttpClient client = _httpClientFactory.CreateClient("PedaleaApiProductos");
                    HttpResponseMessage response = await client.PutAsJsonAsync($"api/productos/UpdateProduct/{id}", model);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(model);
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
                HttpClient client = _httpClientFactory.CreateClient("PedaleaApiProductos");
                HttpResponseMessage response = await client.DeleteAsync($"api/productos/DeleteProduct/{id}");
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
            Producto producto = new();
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("PedaleaApiProductos");
                HttpResponseMessage response = await client.GetAsync($"api/productos/GetProduct/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        producto = JsonSerializer.Deserialize<Producto>(content, options);
                    }
                    return View(producto);
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
