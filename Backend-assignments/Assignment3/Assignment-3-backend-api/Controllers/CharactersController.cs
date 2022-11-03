using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment_3_backend_api.Models;
using AutoMapper;
using Assignment_3_backend_api.Services.Characters;
using Assignment_3_backend_api.Models.DTOs.Character;
using System.Net;

namespace Assignment_3_backend_api.Controllers
{
    [Route("api/v1/characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICharacterService _characterService;

        public CharactersController(IMapper mapper, ICharacterService characterService)
        {
            _mapper = mapper;
            _characterService = characterService;

        }

        /// <summary>
        /// Gets all Characters
        /// </summary>
        /// <returns>a list of characterReadDto objects</returns>

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterReadDto>>> GetCharacters()
        {
            return Ok(_mapper.Map<List<CharacterReadDto>>(await _characterService.GetAllAsync()));
        }
        /// <summary>
        /// Gets a single Character.
        /// </summary>
        /// <param name="id"> character id</param>
        /// <returns> a character object</returns>

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDto>> GetCharacter(int id)
        {
            try
            {
                return Ok(_mapper.Map<CharacterReadDto>(
                        await _characterService.GetByIdAsync(id))
                    );
            }
            catch (Exception ex)
            {
                // Formatting an error code for the exception messages.
                // Using the built in Problem Details.
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }

        }
        /// <summary>
        /// Puts a single character
        /// </summary>
        /// <param name="id"> character id </param>
        /// <param name="character"> characterputDto object </param>
        /// <returns> an IActionResult specifying the result </returns>

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCharacter(int id, CharacterPutDto character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }



            try
            {
                _characterService.UpdateAsync(
                         _mapper.Map<Character>(character)
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

        }

        /// <summary>
        /// Post a single character
        /// </summary>
        /// <param name="characterDto"> characterPost Dto object </param>
        /// <returns> an IActionResult specifying the result </returns>
        /// 
        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCharacter(CharacterPostDto characterDto)
        {
            Character character = _mapper.Map<Character>(characterDto);
            _characterService.AddAsync(character);
            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);

        }


        /// <summary>
        /// Deletes a character
        /// </summary>
        /// <param name="id"> character id </param>
        /// <returns> an IActionResult specifying the result </returns>
        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character =  _characterService.DeleteByIdAsync(id);

            if (character == null)
            {
                return NotFound();
            }



            return NoContent();
        }
    }
}
