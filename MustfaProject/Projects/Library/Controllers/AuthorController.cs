using AutoMapper;
using Core.Entites;
using Core.Interfaces;
using Core.Specs;
using Library.DTOS;
using LibraryBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class AuthorController : BaseApiController
    {
        private readonly IGeneric<Author> _authorRepo;
        private readonly IMapper _mapper;

        public AuthorController(IGeneric<Author> authorRepo, IMapper mapper)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors(string? search, string? sort)
        {
            var spec = new AuthorSpec(search, sort);
            var authors = await _authorRepo.GetAllWithSpec(spec);

            if (authors is null)
            {
                return NotFound(new { Message = "Not Found", StatusCode = 400 });
            }

            // Map Author entities to AuthorDTOs
            var authorDtos = _mapper.Map<IEnumerable<AuthorDTO>>(authors);

            return Ok(authorDtos);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(string name)
        {
            var spec = new AuthorSpec(name);
            var author = await _authorRepo.GetWithSpec(spec);

            if (author is null)
            {
                return NotFound(new { Message = "Not Found", StatusCode = 400 });
            }

            // Map the Author entity to AuthorDTO
            var authorDto = _mapper.Map<AuthorDTO>(author);

            return Ok(authorDto);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> AddAuthor(AuthorDTO authorDto)
        {
            var createdAuthor = new Author
            {
                Name = authorDto.Name,
                Description = authorDto.Description,
                Books = new List<Book>(),
            };

            var spec = new AuthorSpec(authorDto.Name);
            var existingBook = await _authorRepo.GetWithSpec(spec);

            if (existingBook is not null)
                return BadRequest("Author Exits");

            await _authorRepo.AddAsync(createdAuthor);

            var createdAuthorDto = _mapper.Map<AuthorDTO>(createdAuthor);

            return CreatedAtAction(nameof(GetAuthor), new { name = createdAuthor.Name }, createdAuthorDto);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<Author>> DeleteAuthor(string name)
        {
            try
            {

                var spec = new AuthorSpec(name);
                var author = await _authorRepo.GetWithSpec(spec);

                if (author == null)
                {
                    return NotFound(new { Message = "Author not found." }); // Handle the case where the author doesn't exist
                }

                await _authorRepo.DeleteAsync(author.Id); // Delete the author using their ID
                return NoContent(); // Return NoContent (204) on successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }
    }
}
