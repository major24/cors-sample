using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using client_mvc.Models;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace client_mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Call from UI button
        [HttpGet]
        public async Task<string> GetValuesFromApi()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage resp = await client.GetAsync("https://localhost:44347/api/values");
            var sc = resp.EnsureSuccessStatusCode();
            string body = await resp.Content.ReadAsStringAsync();

            return body;
        }

        // Call from UI for products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage resp = await client.GetAsync("https://localhost:44347/api/products");
            var sc = resp.EnsureSuccessStatusCode();
            string body = await resp.Content.ReadAsStringAsync();

            return Json(body);
        }

        // Post product
        [HttpPost]
        public async Task<IActionResult> SaveProduct(Product product)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://localhost:44347/api/products");
            client.DefaultRequestHeaders.Accept.Clear();

            var cont = JsonConvert.SerializeObject(product);

            var buffer = System.Text.Encoding.UTF8.GetBytes(cont);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage messge = client.PostAsync(uri, byteContent).Result;

            string description = string.Empty;

            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = result;
            }

            return Json(description);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
