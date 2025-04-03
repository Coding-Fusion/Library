using AutoMapper;
using Core.Entites;
using Core.Entities;
using Core.Interfaces;
using Core.Specs;
using Library.DTOS;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.api.Attributes;

namespace Library.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly IGeneric<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(IGeneric<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        //[CacheAttributes(30)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories(string? search, string? sort)
        {
            var spec = new CategorySpec(search, sort);
            var categories = await _categoryRepo.GetAllWithSpec(spec);

            if (categories == null)
            {
                return NotFound(new { Message = "Categories not found", StatusCode = 404 });
            }

            
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(categoryDtos);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(string name)
        {
            var spec = new CategorySpec(name);
            var category = await _categoryRepo.GetWithSpec(spec);

            if (category == null)
            {
                return NotFound(new { Message = "Category not found", StatusCode = 404 });
            }


            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }



        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategory(CategoryDTO category)
        {
            var createdCategory = new Category
            {
                Name = category.Name,
            };


            var spec = new CategorySpec(category.Name);
            var existingBook = await _categoryRepo.GetWithSpec(spec);

            if (existingBook is not null)
                return BadRequest("Category Exits");

            await _categoryRepo.AddAsync(createdCategory);


            var createdCategoryDto = _mapper.Map<CategoryDTO>(createdCategory);

            return CreatedAtAction(nameof(GetCategory), new { name = createdCategory.Name }, createdCategoryDto);
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(string id)
        {
            await _categoryRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
