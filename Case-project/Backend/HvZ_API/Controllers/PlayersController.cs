using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Models;
using AutoMapper;
using HvZ_API.Models.DTOs.Player;
using HvZ_API.Utils.Exceptions;
using System.Net;
using HvZ_API.Services;
using HvZ_API.Models.DTOs.CheckIn;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HvZ_API.Controllers
{
    [Authorize(Roles = "User, Admin")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayersController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of All Players in a Game.
        /// </summary>
        /// <param name="gameId"> ID of Game</param>
        /// <returns>A List of All Players in a Game.</returns>
        /// <remarks>
        /// Returns Array of Players in a Game JSON(s)
        /// </remarks>
        /// <response code="200">Returns Array of All Players in a Game, or empty array.</response>
        // GET: api/Players
        [HttpGet("Game/{gameId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PlayerReadDto>>> GetPlayers(int gameId)
        {

            return Ok(
                _mapper.Map<List<PlayerReadDto>>(
                await _playerService.GetAllFromGameAsync(gameId)));
        }

        /// <summary>
        /// Get single Player
        /// </summary>
        /// <param name="id" > Player Id</param>
        /// <returns>A Single Player</returns>
        /// <remarks>
        /// Returns a single Player as JSON
        /// </remarks>
        /// <response code="200">Returns single Player.</response>
        /// <response code="404">Not found, or error message.</response>
        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerReadDto>> GetPlayer(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<PlayerReadDto>(
                    await _playerService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }


        /// <summary>
        /// Update a Player
        /// </summary>
        /// <param name="id" > Player ID to update</param>
        /// <param name="player" > New JSON to update old Player</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Updates/Replaces Player
        /// </remarks>
        /// <response code="204">Returns Nothing</response>
        /// <response code="404">No Player found or Concurrency exception.</response>
        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, PlayerPutDto player)
        {
            try
            {
                await _playerService.UpdateAsync(
                        id, _mapper.Map<Player>(player)
                     );
                return NoContent();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = e.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );

            }
        }

        /// <summary>
        /// Post a new Player
        /// </summary>
        /// <param name="playerPostDto" > New JSON to POST</param>
        /// <returns>Returns POSTed Player</returns>
        /// <remarks>
        ///  Post a new Player
        /// </remarks>
        /// <response code="201">Returns the POSTed Player</response>
        /// <response code="404">Could not POST.</response>
        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPlayer(PlayerPostDto playerPostDto)
        {
            try
            {
                Player player = _mapper.Map<Player>(playerPostDto);
                await _playerService.AddAsync(player);
                return CreatedAtAction("GetPlayer", new { id = player.Id }, _mapper.Map<PlayerReadDto>(player));

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = e.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }

        }

        /// <summary>
        /// Delete a Player 
        /// </summary>
        /// <param name="id" > Id of wanted Player to delete</param>
        /// <returns>Returns Nothing</returns>
        /// <remarks>
        ///  Delete a Player
        /// </remarks>
        /// <response code="204">Deleted Successfully.</response>
        /// <response code="404">Could not Delete.</response>
        // DELETE: api/Missions/5
        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            try
            {
                await _playerService.DeleteByIdAsync(id);
                return NoContent();

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }



        }

        /// <summary>
        /// Get single CheckIn
        /// </summary>
        /// <param name="id" > CheckIn Id</param>
        /// <returns>A Single CheckIn</returns>
        /// <remarks>
        /// Returns a single CheckIn as JSON
        /// </remarks>
        /// <response code="200">Returns single CheckIn.</response>
        /// <response code="404">Not found, or error message.</response>
        [HttpGet("CheckIn/{id}")]
        public async Task<ActionResult<PlayerCheckinReadDto>> GetCheckInPlayer(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<PlayerCheckinReadDto>(
                    await _playerService.GetCheckInAsync(id)));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }

        /// <summary>
        /// Get List of All CheckIns in a SQUAD.
        /// </summary>
        /// <param name="squadId" > Squad Id</param>
        /// <returns>A List of All CheckIns in a SQUAD.</returns>
        /// <remarks>
        /// Returns Array of Players CheckIns in a SQUAD as Array of JSON(s)
        /// </remarks>
        /// <response code="200">Returns Array of all CheckIns in a SQUAD, or empty array.</response>
        /// <response code="404">SquadId Not found.</response>
        [HttpGet("Squad/{squadId}/CheckIn")]
        public async Task<ActionResult<IEnumerable<PlayerCheckinReadDto>>> GetSquadCheckInsPlayer(int squadId)
        {
            try
            {
                return Ok(
                    _mapper.Map<List<PlayerCheckinReadDto>>(
                    await _playerService.GetSquadCheckInsAsync(squadId)));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }

        /// <summary>
        /// Post a new Player CheckIn
        /// </summary>
        /// <param name="checkInPostDto" > New JSON to POST</param>
        /// <returns>Returns POSTed Player CheckIn</returns>
        /// <remarks>
        ///  Post a new Player CheckIn
        /// </remarks>
        /// <response code="201">Returns the POSTed Player CheckIn</response>
        /// <response code="404">Could not POST.</response>
        [HttpPut("CheckIn/{id}")]
        public async Task<IActionResult> PostPlayerCheckin(int id, PlayerCheckinPutDto checkInPostDto)
        {
            try
            {
                Player checkIn = _mapper.Map<Player>(checkInPostDto);
                var checkLock = _mapper.Map<PlayerCheckinReadDto>(checkIn);
                var player = await _playerService.CreateCheckInForPlayerAsync(id, checkIn);

                return CreatedAtAction("GetPlayer", new { id = checkLock.Id }, checkLock);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
            catch (DbUpdateConcurrencyException e)
            {
                return NotFound(
                     new ProblemDetails()
                     {
                         Detail = e.Message,
                         Status = ((int)HttpStatusCode.NotFound)
                     }
                     );
            }

        }

    }
}
