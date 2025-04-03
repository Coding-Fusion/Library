using AutoMapper;
using Core.Interfaces;
using Core.Specs;
using Library.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.api.Attributes;

namespace Library.Controllers
{
    public class BookController : BaseApiController
    {
        private readonly IGeneric<Book> _bookRepo;
        private readonly IMapper _mapper;

        public BookController(IGeneric<Book> bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        //[CacheAttributes(30)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks(string? search, string? sort)
        {

            var spec = new BookSpec(sort, search);
            var books = await _bookRepo.GetAllWithSpec(spec);
            if (books == null || !books.Any())
            {
                return NotFound(new { Message = "Books not found", StatusCode = 404 });
            }


            var bookDtos = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(books);
            return Ok(bookDtos);
        }
        // [CacheAttributes(30)]
        [HttpGet("{title}")]
        public async Task<ActionResult<BookDTO>> GetBookByTitle(string title)
        {

            var spec = new BookSpec(title);
            var book = await _bookRepo.GetWithSpec(spec);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found", StatusCode = 404 });
            }

            var bookDto = _mapper.Map<Book, BookDTO>(book);
            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook(BookDTO bookDto)
        {
            if (bookDto == null)
                return BadRequest(new { Message = "Invalid Book Data", StatusCode = 400 });




            var spec = new BookSpec(bookDto.Title);
            var existingBook = await _bookRepo.GetWithSpec(spec);

            if (existingBook is not null)
                return BadRequest("Book Exits");

            var book = _mapper.Map<BookDTO, Book>(bookDto);

            await _bookRepo.AddAsync(book);

            var createdBookDto = _mapper.Map<Book, BookDTO>(book);


            return CreatedAtAction(nameof(GetBookByTitle), new { title = book.Title }, createdBookDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(string id)
        {
            await _bookRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
