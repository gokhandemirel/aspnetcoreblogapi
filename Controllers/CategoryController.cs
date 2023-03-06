using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnetcoreblogapi.Models;
using aspnetcoreblogapi.Repository;

namespace aspnetcoreblogapi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BlogContext _context;

        public CategoryController(BlogContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Get categories
        /// </summary>
        [Route("/api/category/get")]
        public async Task<IActionResult> GetCategories()
        {
            return Json(await _context.Category.Where(x=> x.IsActive == 1).ToListAsync());
        }


        /// <summary>
        /// Create category
        /// </summary>
        [Route("/api/category/create")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;
            category.IsActive = 1;
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return Json(category);
            }
            return Json(category);
        }


        /// <summary>
        /// Delete tag
        /// </summary>
        [Route("/api/category/delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteCategory([FromBody] Category category)
        {
            var model = await _context.Category.FindAsync(category.Id);
            model.IsActive = 0;
            _context.Category.Update(model);
            await _context.SaveChangesAsync();
            return Json(category);
        }


        /// <summary>
        /// Tag edit
        /// </summary>
        [Route("/api/category/edit")]
        [HttpPost]
        public async Task<IActionResult> EditCategory([FromBody] Category category)
        {
            var model = await _context.Category.FindAsync(category.Id);
            model.Name = category.Name;
            _context.Category.Update(model);
            await _context.SaveChangesAsync();
            return Json(category);
        }


        /// <summary>
        /// Tag ispublish
        /// </summary>
        [Route("/api/category/ispublish")]
        [HttpPost]
        public async Task<IActionResult> PublishCategory([FromBody] Category category)
        {
            var model = await _context.Category.FindAsync(category.Id);
            model.IsPublish = category.IsPublish;
            _context.Category.Update(model);
            await _context.SaveChangesAsync();
            return Json(category);
        }


        /// <summary>
        /// Get tags by id
        /// </summary>
        [Route("/api/category/getbyid")]
        [HttpPost]
        public async Task<IActionResult> GetCategoryById([FromBody] Category category)
        {
            var model = await _context.Category.FirstOrDefaultAsync(m => m.Id == category.Id);
            return Json(model);
        }




    }
}
