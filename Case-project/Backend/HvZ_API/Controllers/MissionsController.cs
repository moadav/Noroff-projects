using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Models;
using HvZ_API.Services;
using HvZ_API.Models.DTOs.Mission;
using AutoMapper;
using HvZ_API.Utils.Exceptions;
using System.Security.Principal;
using System.Net;
using HvZ_API.Models.DTOs.CheckIn;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HvZ_API.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/v1/Games/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _service;
        private readonly IMapper _mapper;

        public MissionsController(IMissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of All Missions
        /// </summary>
        /// <returns>A List of All Missions</returns>
        /// <remarks>
        /// Returns Array of Missions JSON(s)
        /// </remarks>
        /// <response code="200">Returns Array of All Missions, or empty array.</response>
        /// <response code="404">Not found.</response>
        /// <response code="500"></response>
        // GET: api/Missions
        [HttpGet("Game/{gameId}")]
        public async Task<ActionResult<IEnumerable<MissionReadDto>>> GetMissions(int gameId)
        {
            try
            {
                return Ok(_mapper.Map<List<MissionReadDto>>(await _service.GetAllFromGameAsync(gameId)));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get single Mission
        /// </summary>
        /// <param name="id" > Mission Id</param>
        /// <returns>A Single Mission</returns>
        /// <remarks>
        /// Returns a single Mission as JSON
        /// </remarks>
        /// <response code="200">Returns single Mission.</response>
        /// <response code="404">Not found, or error message.</response>
        // GET: api/Missions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MissionReadDto>> GetMission(int id)
        {
            try
            {
                return Ok(_mapper.Map<MissionReadDto>(await _service.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = e.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update a Mission
        /// </summary>
        /// <param name="id" > Mission ID to update</param>
        /// <param name="missionPutDto" > New JSON to update old Mission</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Updates/Replaces Mission
        /// </remarks>
        /// <response code="204">Returns Nothing</response>
        /// <response code="404">No Mission found.</response>
        /// <response code="500"></response>
        // PUT: api/Missions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutMission(int id, MissionPutDto missionPutDto)
        {
            try
            {

                await _service.UpdateAsync(id, _mapper.Map<Mission>(missionPutDto));

                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = e.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Post a new Mission
        /// </summary>
        /// <param name="missionPostDto" > New JSON to POST</param>
        /// <returns>Returns POSTed Mission</returns>
        /// <remarks>
        ///  Post a new Mission
        /// </remarks>
        /// <response code="201">Returns the POSTed Mission</response>
        /// <response code="404">Could not POST.</response>
        //POST: api/Missions
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostMission(MissionPostDto missionPostDto)
        {
            try
            {
            Mission mission = _mapper.Map<Mission>(missionPostDto);
            await _service.AddAsync(mission);
            return CreatedAtAction("GetMission", new { id = mission.Id }, mission);

            }catch(EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }

        /// <summary>
        /// Delete a Mission 
        /// </summary>
        /// <param name="id" > Id of wanted Mission to delete</param>
        /// <returns>Returns Nothing</returns>
        /// <remarks>
        ///  Delete a Mission
        /// </remarks>
        /// <response code="200">Deleted Successfully.</response>
        /// <response code="404">Could not Delete.</response>
        // DELETE: api/Missions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            try
            {
                await _service.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails { Status = (int)HttpStatusCode.NotFound, Detail = e.Message });
            }
        }


    }
}
