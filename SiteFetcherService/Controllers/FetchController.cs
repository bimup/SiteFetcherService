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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return Ok(new { Content = content });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}