using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SafeboardSnake.Core.Engine;
using SafeboardSnake.Core.Models.DataTransferContracts;
using SafeboardSnake.WebApi.ExceptionFilters;
using SafeboardSnake.WebApi.Models;

namespace SafeboardSnake.WebApi.Controllers
{
    /// <summary>
    /// REST API Controller defining api for snake-game
    /// </summary>
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class GameBoardController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameBoardController(GameService game)
        {
            _gameService = game;
        }

        // GET api/gameboard
        [HttpGet]
        [Route("api/gameboard")]
        public IActionResult Get()
        {
            return Ok(_gameService.GetCurrentTurn());
        }

        // GET api/direction/
        // (in body: {"direction": "top"})
        [HttpPost]
        [Route("api/direction/")]
        [ArgumentExceptionFilter(
            ExceptionType = typeof(ArgumentException),
            Message = "Invalid direction descriptor",
            StatusCode = HttpStatusCode.BadRequest)]
        public IActionResult ChangeDirection([FromBody] DirectionDescriptor directionDescriptor)
        {
            if (string.IsNullOrEmpty(directionDescriptor?.Direction))
            {
                return BadRequest();
            }

            _gameService.ChangeDirection(directionDescriptor.Direction);
            return Ok();
        }
    }
}