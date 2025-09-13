using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq; // eller System.Text.Json

public class WeatherController : Controller
{
    private readonly HttpClient _httpClient;

    public WeatherController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        string apiKey = "df78ce59-81df-4086-89de-5089104c2b6a";
        string stationId = "06149";
        string parameter = "temp_dry";

        string url = $"https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?period=latest&parameterId={parameter}&stationId={stationId}&api-key={apiKey}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Kunne ikke hente data fra DMI.";
            return View();
        }

        var jsonString = await response.Content.ReadAsStringAsync();

        // Du kan parse JSON som JObject eller deserialisere til en C# model
        var data = JObject.Parse(jsonString);

        // Her henter vi f.eks. temperaturen fra JSON
        var features = data["features"]?[0];
        var temperature = features?["properties"]?["value"]?.ToString();

        ViewBag.Temperature = temperature;
        return View();
    }
}
