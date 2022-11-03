using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment_3_backend_api.Models;
using AutoMapper;
using Assignment_3_backend_api.Services.Movies;
using Assignment_3_backend_api.Models.DTOs.Movie;
using System.Net;
using Assignment_3_backend_api.Models.DTOs.Character;

namespace Assignment_3_backend_api.Controllers
{
    [Route("api/v1/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public MoviesController(IMapper mapper, IMovieService movieService)
        {
            _mapper = mapper;
            _movieService = movieService;
            
        }


        /// <summary>
        /// Gets a collection of Movies
        /// </summary>
        /// <returns> A collection of movies </returns>
        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetMovies()
        {
            return Ok(_mapper.Map<List<MovieReadDto>>(await _movieService.GetAllAsync()));

        }


        /// <summary>
        /// Gets a single Movie
        /// </summary>
        /// <param name="id"> movie id </param>
        /// <returns> a single movieReadDto object</returns>
        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDto>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper.Map<MovieReadDto>(
                        await _movieService.GetByIdAsync(id))
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
        /// Gets a collection of characters
        /// </summary>
        /// <param name="id"> movie id </param>
        /// <returns> Gets a collection of characters </returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDto>>> GetCharactersForMovieById(int id)
        {
            try
            {
                return Ok(_mapper.Map<List<CharacterReadDto>>(
                       await _movieService.GetAllCharactersAsync(id)
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
        /// Puts a single movie
        /// </summary>
        /// <param name="id"> movie id </param>
        /// <param name="movie"> movieputDto object </param>
        /// <returns> an IActionResult specifying the result </returns>
        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutMovie(int id, MoviePutDto movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }



            try
            {
                _movieService.UpdateAsync(
                         _mapper.Map<Movie>(movie)
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
        /// Post a single movie
        /// </summary>
        /// <param name="movieDto"> moviepostDto object </param>
        /// <returns> an IActionResult specifying the result </returns>
        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostMovie(MoviePostDto movieDto)
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
            _movieService.AddAsync(movie);
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }



        /// <summary>
        /// Deletes a single Movie
        /// </summary>
        /// <param name="id"> movie id </param>
        /// <returns> an IActionResult specifying the result </returns>
        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = _movieService.DeleteByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            return NoContent();
        }


        /// <summary>
        /// Puts a list of characters
        /// </summary>
        /// <param name="id"> movie id </param>
        /// <param name="characterIds"> List of characterIds </param>
        /// <returns> an IActionResult specifying the result </returns>

        [HttpPut("{id}/characters")]
        public IActionResult UpdateCharactersForMovie(int[] characterIds, int id)
        {
            try
            {
                _movieService.UpdateCharactersAsync(characterIds, id);
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
