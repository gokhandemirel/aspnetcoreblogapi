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
    public class TagController : Controller
    {
        private readonly BlogContext _context;

        public TagController(BlogContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Get tags
        /// </summary>
        [Route("/api/tag/get")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _context.Tag.Where(x=> x.IsActive == 1).ToListAsync();
            return Json(tags);
        }


        /// <summary>
        /// Create tag
        /// </summary>
        [Route("/api/tag/create")]
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] Tag tags)
        {
            tags.CreatedDate = DateTime.Now;
            tags.UpdatedDate = DateTime.Now;
            tags.IsActive = 1;
            if (ModelState.IsValid)
            {
                _context.Add(tags);
                await _context.SaveChangesAsync();
                return Json(tags);
            }
            return Json(tags);
        }


        /// <summary>
        /// Delete tag
        /// </summary>
        [Route("/api/tag/delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteTag([FromBody] Tag tag)
        {
            var model = await _context.Tag.FindAsync(tag.Id);
            model.IsActive = 0;
            _context.Tag.Update(model);
            await _context.SaveChangesAsync();
            return Json(tag);
        }


        /// <summary>
        /// Tag edit
        /// </summary>
        [Route("/api/tag/edit")]
        [HttpPost]
        public async Task<IActionResult> EditTag([FromBody] Tag tag)
        {
            var model = await _context.Tag.FindAsync(tag.Id);
            model.Name = tag.Name;
            _context.Tag.Update(model);
            await _context.SaveChangesAsync();
            return Json(tag);
        }


        /// <summary>
        /// Tag ispublish
        /// </summary>
        [Route("/api/tag/ispublish")]
        [HttpPost]
        public async Task<IActionResult> PublishTag([FromBody] Tag tag)
        {
            var model = await _context.Tag.FindAsync(tag.Id);
            model.IsPublish = tag.IsPublish;
            _context.Tag.Update(model);
            await _context.SaveChangesAsync();
            return Json(tag);
        }


        /// <summary>
        /// Get tags by id
        /// </summary>
        [Route("/api/tag/getbyid")]
        [HttpPost]
        public async Task<IActionResult> GetTagById([FromBody] Tag tag)
        {
            var model = await _context.Tag.FirstOrDefaultAsync(m => m.Id == tag.Id);
            return Json(model);
        }


    }
}
