using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _factory;

        public List<SurveyItem> SurveyItems { get; set; }
        public IDictionary<string, string> OrderStats { get; set; }
        public IndexModel(ILogger<IndexModel> logger,
            IHttpClientFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        public async Task<IActionResult> OnGet()
        {
            var rawJson = System.IO.File.ReadAllText("wwwroot/SampleData/survey.json");

            SurveyItems = JsonSerializer.Deserialize<List<SurveyItem>>(rawJson);

            var client = _factory.CreateClient();
            var response = await client
                .GetAsync("https://wiredbraincoffeeadmin.azurewebsites.net/api/orderStats");

            var responseData = await response.Content.ReadAsStringAsync();

            OrderStats = JsonSerializer.Deserialize<IDictionary<string, string>>(responseData);

            return Page();
        }
    }
}