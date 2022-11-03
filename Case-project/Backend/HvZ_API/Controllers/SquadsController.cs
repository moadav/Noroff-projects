using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Models;
using AutoMapper;
using HvZ_API.Models.DTOs.Player;
using HvZ_API.Models.DTOs.Squad;
using HvZ_API.Utils.Exceptions;
using System.Net;
using System.Numerics;
using HvZ_API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HvZ_API.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SquadsController : ControllerBase
    {

        private readonly ISquadService _squadService;
        private readonly IMapper _mapper;

        public SquadsController(ISquadService squadService, IMapper mapper)
        {
            _squadService = squadService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of All Squads in a Game.
        /// </summary>
        /// <param name="id"> ID of Game</param>
        /// <returns>A List of All Squads in a Game.</returns>
        /// <remarks>
        /// Returns Array of Squads in a Game JSON(s)
        /// </remarks>
        /// <response code="200">Returns Array of All Squads in a Game, or empty array.</response>
        /// <response code="404">Game Not found.</response>
        // GET: api/Squads
        [HttpGet("Game/{id}")]
        public async Task<ActionResult<IEnumerable<SquadReadDto>>> GetSquads(int id)
        {
            try
            {
                return Ok(
               _mapper.Map<List<SquadReadDto>>(
               await _squadService.GetAllFromGameAsync(id)));
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
        /// Get single Squad
        /// </summary>
        /// <param name="id" > Squad Id</param>
        /// <returns>A Single Squad</returns>
        /// <remarks>
        /// Returns a single Squad as JSON
        /// </remarks>
        /// <response code="200">Returns single Squad.</response>
        /// <response code="404">Not found, or error message.</response>
        // GET: api/Squads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SquadReadDto>> GetSquad(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<SquadReadDto>(
                    await _squadService.GetByIdAsync(id)));
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
        /// Update a Squad
        /// </summary>
        /// <param name="id" > Squad ID to update</param>
        /// <param name="squad" > New JSON to update old Squad</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Updates/Replaces Squad
        /// </remarks>
        /// <response code="204">Returns Nothing</response>
        /// <response code="404">No Squad found or Concurrency exception.</response>
        // PUT: api/Squads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSquad(int id, SquadPutDto squad)
        {
            try
            {
                await _squadService.UpdateAsync(
                         id, _mapper.Map<Squad>(squad)
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
                return NotFound(new ProblemDetails()
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }


        /// <summary>
        /// Post a new Squad
        /// </summary>
        /// <param name="squadPostDto" > New JSON to POST</param>
        /// <returns>Returns POSTed Squad</returns>
        /// <remarks>
        ///  Post a new Squad
        /// </remarks>
        /// <response code="201">Returns the POSTed Squad</response>
        /// <response code="404">Could not POST.</response>
        // POST: api/Squads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostSquad(SquadPostDto squadPostDto)
        {
            try
            {
                Squad squad = _mapper.Map<Squad>(squadPostDto);
                var read = _mapper.Map<SquadReadDto> (await _squadService.AddAsync(squad));
                return CreatedAtAction("GetSquad", new { id = read.Id }, read);

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
        /// Post a Squad Member
        /// </summary>
        /// <param name="squadId" > Id of Squad</param>
        /// <param name="playerId" > Id of Player</param>
        /// <returns>Returns POSTed Squad</returns>
        /// <remarks>
        ///  Post a new Squad
        /// </remarks>
        /// <response code="200">Squad member added.</response>
        /// <response code="404">Could not POST.</response>
        // POST: api/Squads/player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{squadId}/player/{playerId}")]
        public async Task<IActionResult> PutSquadMember(int squadId, int playerId)
        {
            try
            {
                
                await _squadService.AddSquadMemberAsync(squadId,playerId);
                

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
            return Ok("Player Added");
        }

        /// <summary>
        /// Delete a Squad Member from a Squad
        /// </summary>
        /// <param name="squadId" > Id of Squad</param>
        /// <param name="playerId" > Id of Player</param>
        /// <returns>Returns nothing.</returns>
        /// <remarks>
        ///  Delete a Squad Member from a Squad
        /// </remarks>
        /// <response code="200">Squad member deleted.</response>
        /// <response code="404">Could not delete.</response>
        [HttpDelete("{squadId}/player/{playerId}")]
        public async Task<IActionResult> DeleteSquadMember(int squadId, int playerId)
        {
            try
            {

                await _squadService.RemoveSquadMemberAsync(squadId, playerId);


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
            return Ok("Player Removed");
        }


        /// <summary>
        /// Get List of All Members in a Squad.
        /// </summary>
        /// <param name="squadId" > Squad Id</param>
        /// <returns>A List of All Members in a Squad.</returns>
        /// <remarks>
        /// Returns Array of Members in a Squad as Array of JSON(s)
        /// </remarks>
        /// <response code="200">Returns Array of all Members in a Squad, or empty array.</response>
        /// <response code="404">SquadId Not found.</response>
        // GET: api/Squads/player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("getplayers/{squadId}")]
        public async Task<ActionResult<ICollection<PlayerReadDto>>> GetSquadMember(int squadId)
        {
            try
            {

                return Ok(_mapper.Map<List<PlayerReadDto>> (await _squadService.GetSquadMembersAsync(squadId)));


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
        /// Delete a Squad 
        /// </summary>
        /// <param name="id" > Id of wanted Squad to delete</param>
        /// <returns>Returns Nothing</returns>
        /// <remarks>
        ///  Delete a Squad
        /// </remarks>
        /// <response code="204">Deleted Successfully.</response>
        /// <response code="404">Could not Delete.</response>
        // DELETE: api/Squads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSquad(int id)
        {
            try
            {
                await _squadService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = e.Message,
                    Status = ((int)HttpStatusCode.NotFound)
                }
                );
            }

        }
    }
}
