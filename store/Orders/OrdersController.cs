using Microsoft.AspNetCore.Mvc;

namespace store.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly HttpClient _httpClient;

    public OrdersController(ILogger<OrdersController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IEnumerable<Todo>?> Get()
    {
        var response = await _httpClient.GetAsync("http://localhost:5211/todos");

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Todo>>();

        return result;
    }
}
