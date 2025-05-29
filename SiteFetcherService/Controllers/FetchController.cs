using Microsoft.AspNetCore.Mvc;

namespace SiteFetcherService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FetchController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public FetchController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36 Edg/136.0.0.0");
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("URL не указан");

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return Ok(new { Content = content });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }
    }
}