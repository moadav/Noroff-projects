using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Models;
using HvZ_API.Contexts;
using HvZ_API.Models.DTOs.Gravestone;
using HvZ_API.Services;
using AutoMapper;
using System.Collections;
using HvZ_API.Models.DTOs.Mission;
using HvZ_API.Utils.Exceptions;
using System.Net;
using HvZ_API.Models.DTOs.Game;
using HvZ_API.Models.DTOs.GameConfig;
using HvZ_API.Models.DTOs.Player;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HvZ_API.Controllers
{
    [Authorize(Roles = "User, Admin")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public GamesController(IGameService context, IMapper mapper)
        {
            _service = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of All Games
        /// </summary>
        /// <returns>A List of All Games Files</returns>
        /// <remarks>
        /// Returns List of Game JSON(s)
        /// </remarks>
        /// <response code="200">Returns List of All Games, or empty array.</response>
        // GET: api/Games
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<GameReadDto>>> GetGame()
        {
            return Ok((_mapper.Map<List<GameReadDto>>(await _service.GetAllAsync())));
        }

        /// <summary>
        /// Get a Sigle Game
        /// </summary>
        /// <param name="id" > ID for wanted Game</param>
        /// <returns>Returns a Single Game</returns>
        /// <remarks>
        /// Returns a Sigle Game Config File
        /// </remarks>
        /// <response code="200">Returns a Single Game</response>
        /// <response code="404">No Game found with ID.</response>
        // GET: api/Games/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GameReadDto>> GetGame(int id)
        {
            try
            {

            return Ok(_mapper.Map<GameReadDto>(await _service.GetByIdAsync(id)));

            }catch(EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails { Detail = e.Message, Status = 404 });

            }
        }

        /// <summary>
        /// Update a Game
        /// </summary>
        /// <param name="id" > ID for wanted Game</param>
        /// <param name="game" > New JSON to update old Game</param>
        /// <returns>nothing</returns>
        /// <remarks>
        /// Updates/Replaces Game with particular ID
        /// </remarks>
        /// <response code="204">Returns Nothing</response>
        /// <response code="404">No Game found.</response>
        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GamePutDto game)
        {

            try
            {
                await _service.UpdateAsync(id, _mapper.Map<Game>(game));
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails { Detail = e.Message, Status = 404 });

            }

        }

        /// <summary>
        /// Post a new Game
        /// </summary>
        /// <param name="gameDto" > New JSON to POST</param>
        /// <returns>Returns the Posted Game</returns>
        /// <remarks>
        ///  Post a new Game
        /// </remarks>
        /// <response code="201">Returns the Posted Game</response>
        /// <response code="404">Could not POST.</response>
        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostGame(GamePostDto gameDto)
        {
            try
            {
                var game = await _service.AddAsync(_mapper.Map<Game>(gameDto));

                return CreatedAtAction("GetGame", new { id = game.Id }, game);

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }

        /// <summary>
        /// Delete a Game 
        /// </summary>
        /// <param name="id" > Id of wanted Game to delete</param>
        /// <returns>Returns Nothing</returns>
        /// <remarks>
        ///  Delete a game
        /// </remarks>
        /// <response code="200">Delete Successfull.</response>
        /// <response code="404">Could not Delete.</response>
        // DELETE: api/Games/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                await _service.DeleteByIdAsync(id);
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

            return Ok("Deleted GameConfig with id" + id.ToString());
            
        }

        /// <summary>
        /// Get List of All Victims in a game
        /// </summary>
        /// <param name="gameId" > GameId</param>
        /// <returns>A List of All Victims in a game</returns>
        /// <remarks>
        /// Returns List of Victims in a game as array of JSON(s)
        /// </remarks>
        /// <response code="200">Returns List of All Victims in a game, or empty array.</response>
        /// <response code="404">Returns List of All Games, or empty array.</response>
        // GET: api/Games
        [HttpGet("PlayerVictims/{gameId}")]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<PlayerReadDto>>> GetGameVictims(int gameId)
        {
            try
            {
                return Ok(_mapper.Map<List<PlayerReadDto>>(await _service.GetAllVictims(gameId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// Get List of All Gravestone Victims in a game
        /// </summary>
        /// <param name="gameId" > GameId</param>
        /// <returns>A List of All Gravestone Victims in a game</returns>
        /// <remarks>
        /// Returns List of Gravestone Victims in a game as array of JSON(s)
        /// </remarks>
        /// <response code="200">Returns List of All Gravestones in a game, or empty array.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpGet("{gameId}/GravestoneVictims")]
        [AllowAnonymous]
        public async Task<ActionResult<GravestoneReadDto>> GetGameVictimsGrave(int gameId)
        {
            try
            {
                return Ok(_mapper.Map<List<GravestoneReadDto>>(await _service.GetAllVictimstones(gameId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// Get single Gravestone Victim in a Game
        /// </summary>
        /// <param name="gameId" > GameId</param>
        /// <param name="playerId" > Player Id</param>
        /// <returns>A Single Gravestone Victim in a Game</returns>
        /// <remarks>
        /// Returns a single Victim in a game as JSON
        /// </remarks>
        /// <response code="200">Returns single Victim Gravestones in a Game, or empty JSON.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpGet("{gameId}/GravestoneVictim/{playerId}")]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<GravestoneReadDto>>> GetGameVictimGrave(int gameId, int playerId)
        {
            try
            {
                return Ok(_mapper.Map<GravestoneReadDto>(await _service.GetVictimstone(gameId, playerId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// Get single Gravestone Victim in a Game
        /// </summary>
        /// <param name="gameId" > GameId</param>
        /// <param name="playerId" > Player Id</param>
        /// <returns>A Single Gravestone Victim in a Game</returns>
        /// <remarks>
        /// Returns a single Victim in a game as JSON
        /// </remarks>
        /// <response code="200">Returns single Victim Gravestones in a Game, or empty JSON.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpGet("{gameId}/Players/{playerId}/Kills")]
        public async Task<ActionResult<int>> GetGameVictims(int gameId, int playerId)
        {
            try
            {
                return Ok(_mapper.Map<int>(await _service.GetAllPlayerKillCount(gameId, playerId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// POST single Gravestone
        /// </summary>
        /// <param name="killPlayer" > Gravestone JSON to post</param>
        /// <returns>A Single Gravestone Victim in a Game</returns>
        /// <remarks>
        /// POST a single Gravestone JSON
        /// </remarks>
        /// <response code="200">Returns POSTed Gravestone.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpPost("Kill")]
        public async Task<IActionResult> PostGameKillerstone(GravestoneKillPostDto killPlayer)
        {
            try
            {

                var a = await _service.CreateKillerStone(killPlayer);
                return Ok(_mapper.Map<GravestoneReadDto>(_mapper.Map<Gravestone> (a)));
            
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// POST single Empty Gravestone
        /// </summary>
        /// <param name="killPlayer" > Gravestone JSON to post</param>
        /// <returns>A Single Empty Gravestone in a Game</returns>
        /// <remarks>
        /// POST a single Empty Gravestone JSON
        /// </remarks>
        /// <response code="200">Returns POSTed Gravestone.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpPost("EmptyKill")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GravestoneReadDto>> PostGameEmptyKillerstone(GravestoneKillPostDto killPlayer)
        {
            try
            {

                var a = await _service.CreateKillerStone(killPlayer);
                return Ok(_mapper.Map<GravestoneReadDto>(_mapper.Map<Gravestone>(a)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// Get List of Humans alive in a Game
        /// </summary>
        /// <param name="gameId" > Current Game </param>
        /// <returns>A List of Humans in a Game</returns>
        /// <remarks>
        /// Returns a List of Humans in a Game as array of JSON
        /// </remarks>
        /// <response code="200">Returns  List of Humans in a Game, or empty Array.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpGet("{gameId}/Humans")]
        public async Task<ActionResult<ICollection<PlayerReadDto>>> GetAllHumans(int gameId)
        {
            try
            {
                return Ok(_mapper.Map<List<PlayerReadDto>>(await _service.GetAllHumansInGame(gameId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// Get List of Zombies in a Game
        /// </summary>
        /// <param name="gameId" > Current Game </param>
        /// <returns>A List of Zombies in a Game</returns>
        /// <remarks>
        /// Returns a List of Zombies in a Game as array of JSON
        /// </remarks>
        /// <response code="200">Returns  List of Zombies in a Game, or empty Array.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpGet("{gameId}/Zombies")]
        public async Task<ActionResult<ICollection<PlayerReadDto>>> GetAllZombies(int gameId)
        {
            try
            {
                return Ok(_mapper.Map<List<PlayerReadDto>>(await _service.GetAllZombiesInGame(gameId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// Get List of Patein Zero(s) in a Game
        /// </summary>
        /// <param name="gameId" > Current Game </param>
        /// <returns>A List of Patein Zero(s) in a Game</returns>
        /// <remarks>
        /// Returns a List of Patein Zero(s) in a Game as array of JSON
        /// </remarks>
        /// <response code="200">Returns  List of Patein Zero(s) in a Game, or empty Array.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpGet("{gameId}/PatientZero")]
        public async Task<ActionResult<ICollection<PlayerReadDto>>> GetAllPatientZero(int gameId)
        {
            try
            {
                return Ok(_mapper.Map<List<PlayerReadDto>>(await _service.GetAllPatientZeroInGame(gameId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// Get single Human in a Game
        /// </summary>
        /// <param name="gameId" > GameId</param>
        /// <param name="playerId" > Human/Player Id</param>
        /// <returns>A Single Human in a Game</returns>
        /// <remarks>
        /// Returns a single Human in a game as JSON
        /// </remarks>
        /// <response code="200">Returns single Human in a Game.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpGet("{gameId}/Humans/{playerId}")]
        public async Task<ActionResult<PlayerReadDto>> GetOneHuman(int gameId, int playerId)
        {
            try
            {
                return Ok(_mapper.Map<PlayerReadDto>(await _service.GetOneHumanInGame(gameId,playerId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }

        /// <summary>
        /// Get single Zombie in a Game
        /// </summary>
        /// <param name="gameId" > GameId</param>
        /// <param name="playerId" > Zombie/Player Id</param>
        /// <returns>A Single Zombie in a Game</returns>
        /// <remarks>
        /// Returns a single Zombie in a game as JSON
        /// </remarks>
        /// <response code="200">Returns single Zombie in a Game.</response>
        /// <response code="404">Returns nothing or error message.</response>
        [HttpGet("{gameId}/Zombies/{playerId}")]
        public async Task<ActionResult<PlayerReadDto>> GetOneZombie(int gameId, int playerId)
        {
            try
            {
                return Ok(_mapper.Map<PlayerReadDto>(await _service.GetOneZombieInGame(gameId, playerId)));

            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });

            }

        }
    }
}
