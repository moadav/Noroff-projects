using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Models;
using AutoMapper;
using HvZ_API.Models.DTOs.Chat;
using HvZ_API.Utils.Exceptions;
using System.Net;
using HvZ_API.Services;
using Microsoft.AspNetCore.Authorization;

namespace HvZ_API.Controllers
{
    [Authorize(Roles = "User, Admin")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        public ChatsController(IChatService chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;

        }

        /// <summary>
        /// Get a single chat element
        /// </summary>
        /// <param name="id">Chat Id</param>
        /// <returns>A Chat element</returns>
        /// <remarks>
        /// Returns a Chat JSON
        /// </remarks>
        /// <response code="200">Returns the Chat Element</response>
        /// <response code="404">Does not match any Chat Element</response>
        // GET: api/Ranks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatReadDto>> GetChat(int id)
        {
            try
            {
                return Ok(
                     _mapper.Map<ChatReadDto>(
                     await _chatService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ProblemDetails { Detail = e.Message, Status = 404 });
            }
        }

        /// <summary>
        /// Get a List of Squad Chat element(s)
        /// </summary>
        /// <param name="gameId" >Current GameID</param>
        /// <param name="squadId" >Current SquadID</param>
        /// <returns>A List of Squad Chat element(s)</returns>
        /// <remarks>
        /// Returns a List of squad Chat JSON(s)
        /// </remarks>
        /// <response code="200">Returns the List of Chat elements</response>
        /// <response code="404">No chats found</response>
        [HttpGet("{gameId}/Squad/{squadId}")]
        public async Task<ActionResult<IEnumerable<ChatReadDto>>> GetSquadChat(int gameId, int squadId)
        {
            try
            {
                return Ok(
                    _mapper.Map<List<ChatReadDto>>(
                    await _chatService.GetAllSquadMessageAsync(gameId, squadId)));
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
        /// Get a List of all Zombie Chat element(s) in a game
        /// </summary>
        /// <param name="gameId" >Current GameID</param>
        /// <returns>A List of Zombie Chat element(s)</returns>
        /// <remarks>
        /// Returns a List of Zombie Chat JSON(s)
        /// </remarks>
        /// <response code="200">Returns the List of Zombie Chat elements</response>
        /// <response code="404">No chat's found</response>
        [HttpGet("{gameId}/ZombieChat")]
        public async Task<ActionResult<IEnumerable<ChatReadDto>>> GetZombieChat(int gameId)
        {
            try
            {
                return Ok(
                    _mapper.Map<List<ChatReadDto>>(
                    await _chatService.GetAllLocalZombieMessageAsync(gameId)));
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
        /// Get a List of all Human Chat element(s) in a game
        /// </summary>
        /// <param name="gameId" >Current GameID</param>
        /// <returns>A List of Human Chat element(s)</returns>
        /// <remarks>
        /// Returns a List of Human Chat JSON(s)
        /// </remarks>
        /// <response code="200">Returns the List of Human Chat elements</response>
        /// <response code="404">No chat's found</response>
        [HttpGet("{gameId}/HumanChat")]
        public async Task<ActionResult<IEnumerable<ChatReadDto>>> GetHumanChat(int gameId)
        {
            try
            {
                return Ok(
                    _mapper.Map<List<ChatReadDto>>(
                    await _chatService.GetAllLocalHumanMessageAsync(gameId)));
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
        /// Get a List of all Global Chat element(s) in a game
        /// </summary>
        /// <param name="gameId" >Current GameID</param>
        /// <returns>A List of Global Chat element(s)</returns>
        /// <remarks>
        /// Returns a List of Global Chat JSON(s)
        /// </remarks>
        /// <response code="200">Returns the List of Global Chat elements</response>
        /// <response code="404">No chat's found</response>
        [HttpGet("{gameId}/Global")]
        public async Task<ActionResult<IEnumerable<ChatReadDto>>> GetGlobalChat(int gameId)
        {
            try
            {
                return Ok(
                    _mapper.Map<List<ChatReadDto>>(
                    await _chatService.GetAllGlobalMessageAsync(gameId)));
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
        /// Post a Human Chat Element
        /// </summary>
        /// <param name="chatDto" >Required JSON body</param>
        /// <returns>Returns ChatId</returns>
        /// <remarks>
        /// Post Chat using Required JSON body.
        /// </remarks>
        /// <response code="201">Posted Successfully, Returns ChatId</response>
        /// <response code="404">Could not post chat</response>
        [HttpPost("Local/Human")]
        public async Task<ActionResult> PostChat(ChatPostHumanDto chatDto)
        {
            try
            {

                Chat chat = _mapper.Map<Chat>(chatDto);
                var read = _mapper.Map<ChatReadDto>(await _chatService.AddAsync(chat));
                return CreatedAtAction("GetChat", new { id = read.Id }, read);

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
        /// Post a Zombie Chat Element
        /// </summary>
        /// <param name="chatDto" >Required JSON body</param>
        /// <returns>Returns ChatId</returns>
        /// <remarks>
        /// Post Chat using Required JSON body.
        /// </remarks>
        /// <response code="201">Posted Successfully, Returns ChatId</response>
        /// <response code="404">Could not post chat</response>
        [HttpPost("Local/Zombie")]
        public async Task<ActionResult> PostChat(ChatPostZombieDto chatDto)
        {
            try
            {

                Chat chat = _mapper.Map<Chat>(chatDto);
                var read = _mapper.Map<ChatReadDto>(await _chatService.AddAsync(chat));
                return CreatedAtAction("GetChat", new { id = read.Id }, read);

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
        /// Post a Squad Chat Element
        /// </summary>
        /// <param name="chatDto" >Required JSON body</param>
        /// <returns>Returns ChatId</returns>
        /// <remarks>
        /// Post Chat using Required JSON body.
        /// </remarks>
        /// <response code="201">Posted Successfully, Returns ChatId</response>
        /// <response code="404">Could not post chat</response>
        [HttpPost("Squad")]
        public async Task<ActionResult> PostChat(ChatPostSquadDto chatDto)
        {
            try
            {

                Chat chat = _mapper.Map<Chat>(chatDto);
                var read = _mapper.Map<ChatReadDto>(await _chatService.AddAsync(chat));
                return CreatedAtAction("GetChat", new { id = read.Id }, read);

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
        /// Post a Global Chat Element
        /// </summary>
        /// <param name="chatDto" >Required JSON body</param>
        /// <returns>Returns ChatId</returns>
        /// <remarks>
        /// Post Chat using Required JSON body.
        /// </remarks>
        /// <response code="201">Posted Successfully, Returns ChatId</response>
        /// <response code="404">Could not post chat</response>
        [HttpPost("Player/Global")]
        public async Task<ActionResult> PostChat(ChatPostGlobalDto chatDto)
        {
            try
            {
                Chat chat = _mapper.Map<Chat>(chatDto);
                var read = _mapper.Map<ChatReadDto>(await _chatService.AddAsync(chat));
                return CreatedAtAction("GetChat", new { id = read.Id }, read);

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
        /// Delete a Chat Element
        /// </summary>
        /// <param name="id" >ID of Chat element</param>
        /// <returns>Returns NoContent</returns>
        /// <remarks>
        /// Deletes a chat element in Global, Squad, Zombie and players.
        /// </remarks>
        /// <response code="204">Deleted Chat Successfully</response>
        /// <response code="404">Did not find chat element to delete</response>
        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(int id)
        {
            try
            {
                await _chatService.DeleteByIdAsync(id);
                return NoContent();

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = e.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    });
            }

        }


    }
}
