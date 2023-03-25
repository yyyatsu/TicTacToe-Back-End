using Microsoft.AspNetCore.Mvc;
using TTT.Data.Entities;
using TTT.Data.Enums;
using TTT.Domain.Models;
using TTT.Domain.Services.Interfaces;

namespace TTT.WebAPI.Controllers
{
  [ApiController]
  [Route("Statistics")]
  public class StatisticsController : ControllerBase
  {
    private readonly IStatisticsService statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
      this.statisticsService = statisticsService;
    }

    [HttpGet("GetStatistics/{name}")]
    public async Task<IActionResult> GetStatisticsAsync(string name)
    {
      Statistics statistics = await statisticsService.GetStatisticsAsync(name);

      StatisticsDTO statisticsDTO = new(statistics, name);

      return Ok(statisticsDTO);
    }
  }
}
