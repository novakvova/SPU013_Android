using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Data.Entites;
using WebShop.Helpers;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;

        public CategoriesController(AppEFContext appEFContext, IMapper mapper)
        {
            _mapper = mapper;
            _appEFContext = appEFContext;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var model = await _appEFContext.Categories
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Priority)
                .Select(x => _mapper.Map<CategoryItemViewModel>(x))
                .ToListAsync();
           
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _appEFContext.Categories
                .Where(x => x.IsDeleted == false && x.Id == id)
                .Select(x => _mapper.Map<CategoryItemViewModel>(x))
                .ToListAsync();
            if(model.Count==0)
                return NotFound();

            return Ok(model[0]);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateItemVM model)
        {
            try
            {
                var cat = _mapper.Map<CategoryEntity>(model);
                cat.Image = ImageWorker.SaveImage(model.ImageBase64);
                await _appEFContext.Categories.AddAsync(cat);
                await _appEFContext.SaveChangesAsync();
                return Ok(_mapper.Map<CategoryItemViewModel>(cat));
            }
            catch (Exception ex)
            {

                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] CategoryEditItemVM model)
        {
            try
            {
                var cat = await _appEFContext.Categories.FindAsync(model.Id);
                if(cat==null)
                    return NotFound();
                else
                {
                    cat.Name = model.Name;
                    cat.Description = model.Description;
                    cat.Priority= model.Priority;
                    if(!string.IsNullOrEmpty(model.ImageBase64))
                    {
                        ImageWorker.RemoveImage(cat.Image);
                        cat.Image = ImageWorker.SaveImage(model.ImageBase64);
                    }
                    
                    _appEFContext.Categories.Update(cat);
                    await _appEFContext.SaveChangesAsync();
                    return Ok(_mapper.Map<CategoryItemViewModel>(cat));
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _appEFContext.Categories.FindAsync(id);
            if (cat == null)
                return NotFound();
            cat.IsDeleted = true;
            _appEFContext.SaveChanges();
            return Ok();
        }
    }
}
