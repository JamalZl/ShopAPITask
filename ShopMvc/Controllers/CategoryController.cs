using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMvc.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopMvc.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            CategoryListDto listDto;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44397/admin/api/categories");

                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listDto = JsonConvert.DeserializeObject<CategoryListDto>(responseStr);
                    return View(listDto);
                }
            }


            return RedirectToAction("index", "home");
        }
    }
}
