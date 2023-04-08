using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<CategoryItemViewModel>()
            {
                new CategoryItemViewModel
                {
                    Id = 1,
                    Name = "Ноутбуки",
                    Image = "/images/1.jpg"
                },
                new CategoryItemViewModel
                {
                    Id = 2,
                    Name = "Відеокарти",
                    Image = "/images/2.jpg"
                },
                new CategoryItemViewModel
                {
                    Id = 3,
                    Name = "Оперативна пам'ять",
                    Image = "/images/3.jpg"
                }
            };
           
            return Ok(list);
        }
    }
}
