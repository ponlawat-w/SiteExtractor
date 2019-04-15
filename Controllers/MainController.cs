using Microsoft.AspNetCore.Mvc;
using SiteExtractor.HtmlExtractor;

namespace SiteExtractor.Controllers
{
    public class MainController : Controller
    {
        [HttpGet("{encodedUrl}/{query}/{finalQuery}/display")]
        public IActionResult Display(string encodedUrl, string query, string finalQuery)
        {
            Parser parser = new Parser(encodedUrl, query, finalQuery, true);
            return new OkObjectResult(parser.Execute());
        }

        [HttpGet("{encodedUrl}/{query}/{finalQuery}/redirect")]
        public IActionResult Redirect(string encodedUrl, string query, string finalQuery)
        {
            Parser parser = new Parser(encodedUrl, query, finalQuery, true);
            return Redirect(parser.Execute());
        }
    }
}