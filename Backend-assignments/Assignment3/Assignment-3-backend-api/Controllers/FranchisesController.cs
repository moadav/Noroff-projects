
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment_3_backend_api.Models;
using Assignment_3_backend_api.Services.Characters;
using AutoMapper;
using Assignment_3_backend_api.Services.Franchises;
using Assignment_3_backend_api.Models.DTOs.Character;
using Assignment_3_backend_api.Models.DTOs.Franchise;
using System.Net;
using Assignment_3_backend_api.Models.DTOs.Movie;

namespace Assignment_3_backend_api.Controllers
{
    [Route("api/v1/franchises")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFranchiseService _franchiseService;

        public FranchisesController(IMapper mapper, IFranchiseService franchiseService)
        {
            _mapper = mapper;
            _franchiseService = franchiseService;
        }


        /// <summary>
        /// Gets all franchises
        /// </summary>
        /// <returns> Gets a Collection of franchises </returns>
        // GET: api/Franchises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDto>>> GetFranchises()
        {
            return Ok(_mapper.Map<List<FranchiseReadDto>>(await _franchiseService.GetAllAsync()));

        }

        /// <summary>
        /// Gets a single franchise
        /// </summary>
        /// <param name="id"> franchise id </param>
        /// <returns> Gets a single franchise object </returns>
        // GET: api/Franchises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDto>> GetFranchise(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseReadDto>(
                        await _franchiseService.GetByIdAsync(id))
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
        /// Puts a single franchise
        /// </summary>
        /// <param name="id"> franchise id </param>
        /// <param name="franchise"> franchiseputDto object </param>
        /// <returns> an IActionResult specifying the result </returns>
        // PUT: api/Franchises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutFranchise(int id, FranchisePutDto franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }



            try
            {
                _franchiseService.UpdateAsync(
                         _mapper.Map<Franchise>(franchise)
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
        /// Posts a single character
        /// </summary>
        /// <param name="franchiseDto"> franchisePostDto object </param>
        /// <returns> an IActionResult specifying the result </returns>
        // POST: api/Franchises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostFranchise(FranchisePostDto franchiseDto)
        {
            Franchise franchise = _mapper.Map<Franchise>(franchiseDto);
            _franchiseService.AddAsync(franchise);
            return CreatedAtAction("GetFranchise", new { id = franchise.Id }, franchise);
        }


        /// <summary>
        /// Deletes a single Franchise
        /// </summary>
        /// <param name="id"> franchise id </param>
        /// <returns> an IActionResult specifying the result </returns>

        // DELETE: api/Franchises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            var character = _franchiseService.DeleteByIdAsync(id);

            if (character == null)
            {
                return NotFound();
            }



            return NoContent();

        }



        /// <summary>
        /// Gets a collection of movies
        /// </summary>
        /// <param name="id"> Movie Id </param>
        /// <returns> an collection of movieReadDto </returns>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetMoviesForFranchiseById(int id)
        {
            try
            {
                return Ok(_mapper.Map<List<MovieReadDto>>(
                       await _franchiseService.GetAllMoviesAsync(id)
                   ));
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
        /// Gets a collection of characters
        /// </summary>
        /// <param name="id"> franchise id </param>
        /// <returns> a collection of characterReadDto</returns>

        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDto>>> GetCharactersForFranchiseById(int id)
        {
            try
            {
                return Ok(_mapper.Map<List<CharacterReadDto>>(
                       await _franchiseService.GetAllCharactersAsync(id)
                   ));
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
        /// Puts a collection of movies for franchises
        /// </summary>
        /// <param name="id"> Franchise id </param>
        /// <param name="movieIds"> A list of movie ids </param>
        /// <returns> an IActionResult specifying the result </returns>
        [HttpPut("{id}/movies")]
        public IActionResult UpdateMoviesForFranchise(int[] movieIds, int id)
        {
            try
            {
                _franchiseService.UpdateMoviesAsync(movieIds, id);
                return NoContent();
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


    }
}
