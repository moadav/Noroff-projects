using Microsoft.AspNetCore.Mvc;
using HvZ_API.Models;
using HvZ_API.Contexts;
using HvZ_API.Services;
using AutoMapper;
using HvZ_API.Models.DTOs.GameConfig;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HvZ_API.Controllers
{
    [Authorize(Roles = "User, Admin")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameConfigsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IGameConfigService _service;

        public GameConfigsController(IMapper mapper, IGameConfigService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// Get All Game Config Files
        /// </summary>
        /// <returns>A List of All Game Config Files</returns>
        /// <remarks>
        /// Returns List of Game Config JSON
        /// </remarks>
        /// <response code="200">Returns List of All Game Config Files, or empty array.</response>
        // GET: api/GameConfigs
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerator<GameConfigReadDto>>> GetGameConfig()
        {
            return Ok(_mapper.Map<List<GameConfigReadDto>>(await _service.GetAllAsync()));
        }

        /// <summary>
        /// Get a Sigle Game Config File
        /// </summary>
        /// <param name="id" > ID for wanted config</param>
        /// <returns>Single Config File</returns>
        /// <remarks>
        /// Returns a Sigle Game Config File
        /// </remarks>
        /// <response code="200">Returns a Sigle Game Config File</response>
        /// <response code="404">No GameConfig found.</response>
        // GET: api/GameConfigs/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GameConfigReadDto>> GetGameConfig(int id)
        {
            try
            {
                return Ok(_mapper.Map<GameConfigReadDto>(await _service.GetByIdAsync(id)));

            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }

        }

        /// <summary>
        /// Update a Game Config File
        /// </summary>
        /// <param name="id" > ID for wanted config</param>
        /// <param name="gameConfig" > New JSON to update old object</param>
        /// <returns>nothing</returns>
        /// <remarks>
        /// Updates/Replaces Config file with particular ID
        /// </remarks>
        /// <response code="200">Returns comfirmation string</response>
        /// <response code="404">No GameConfig found.</response>
        // PUT: api/GameConfigs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameConfig(int id, GameConfigPutDto gameConfig)
        {
            try
            {
                await _service.UpdateAsync(id, _mapper.Map<GameConfig>(gameConfig));

            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }

            return Ok("Gameconfig atempted to update");
        }


        /// <summary>
        /// Post a new Game Config File
        /// </summary>
        /// <param name="gameConfig" > New JSON to POST</param>
        /// <returns>Returns the Posted Config file</returns>
        /// <remarks>
        ///  Post a new Game Config File
        /// </remarks>
        /// <response code="201">Returns the Posted Config file</response>
        /// <response code="404">Could not POST.</response>
        // POST: api/GameConfigs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostGameConfig(GameConfigPostDto gameConfig)
        {
            try
            {
                GameConfig Config = await _service.AddAsync(_mapper.Map<GameConfig>(gameConfig));
                var read = _mapper.Map<GameConfigReadDto>(Config);
                return CreatedAtAction("GetGameConfig", new { id = read.Id }, read);
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

        }

        /// <summary>
        /// Delete a Game Config File
        /// </summary>
        /// <param name="id" > Id of wanted Config File to delete</param>
        /// <returns>Returns comfirmation string</returns>
        /// <remarks>
        ///  Post a new Game Config File
        /// </remarks>
        /// <response code="201">Returns error message</response>
        /// <response code="404">Could not Delete.</response>
        // DELETE: api/GameConfigs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameConfig(int id)
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

    }
}
