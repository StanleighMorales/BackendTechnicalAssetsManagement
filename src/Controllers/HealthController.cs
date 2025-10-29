using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/health")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        /// <summary>
        /// Gets the health status of the application and its dependencies.
        /// </summary>
        /// <returns>A detailed health report.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(HealthReport), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HealthReport), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> Get()
        {
            // Manually run all registered health checks
            var report = await _healthCheckService.CheckHealthAsync();

            // Return a 200 OK if the status is Healthy
            if (report.Status == HealthStatus.Healthy)
            {
                return Ok(report);
            }

            // Otherwise, return a 503 Service Unavailable with the detailed report
            return StatusCode(StatusCodes.Status503ServiceUnavailable, report);
        }
    }
}