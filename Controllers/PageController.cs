using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using site_cms_mvc.Models;

namespace site_cms_mvc.Controllers
{
    public class PageController : Controller
    {
        [Route("/")]
        public async Task<ActionResult> Index()
        {
            HttpClient http = new HttpClient();
            var response = await http.GetAsync($"{Program.ApiHost}/api/paginas/home.json");
            return render(response);
        }

        [Route("/{slug}")]
        public async Task<ActionResult> IndexPaths(string slug)
        {
            HttpClient http = new HttpClient();
            var response = await http.GetAsync($"{Program.ApiHost}/api/paginas/{slug}.json");
            return render(response);
        }

        private ContentResult render(HttpResponseMessage response){
            if(response.IsSuccessStatusCode)
            {
                var result  = response.Content.ReadAsStringAsync().Result;
                var pagina = JsonConvert.DeserializeObject<Pagina>(result);
                Response.ContentType = "text/html;";
                return Content(pagina.Conteudo);
            }

            return Content("Pagina não encontrada");
        }
    }
}
