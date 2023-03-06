using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnetcoreblogapi.Models;
using aspnetcoreblogapi.Repository;
using aspnetcoreblogapi.ViewModel;

namespace aspnetcoreblogapi.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogContext _context;

        private readonly IPostRepository _postRepository;
        public PostsController(BlogContext context, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _context = context;
        }


        /// <summary>
        /// Get post
        /// </summary>
        [Route("/api/post/get")]
        public async Task<IActionResult> GetPost()
        {
            var list = await _postRepository.GetAll();

            var result = await _context.Post.Where(x => x.IsActive == 1).Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                IsActive = x.IsActive,
                IsPublish = x.IsPublish,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Categories = x.PostCategory.Select(y => new
                {
                    Id = y.CategoryId,
                    Name = _context.Category.Where(x => x.Id == y.CategoryId).FirstOrDefault().Name
                }),
                Tags = x.PostTag.Select(y => new
                {
                    Id = y.TagId,
                    Name = _context.Tag.Where(x => x.Id == y.TagId).FirstOrDefault().Name
                }),
            }).ToListAsync();

            return Json(result);
        }


        /// <summary>
        /// Create post
        /// </summary>
        [Route("/api/post/create")]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostViewModel request)
        {
            var post = new Post();

            if (ModelState.IsValid)
            {
                post.Title = request.Title;
                post.Content = request.Content;
                post.CreatedDate = DateTime.Now;
                post.UpdatedDate = DateTime.Now;
                post.IsActive = 1;

                List<PostCategory> list = new List<PostCategory>();
                list = request.CategoryIds.Select(x => AddPostCategories(x, post.Id)).ToList();
                post.PostCategory = list;

                List<PostTag> tags = new List<PostTag>();
                tags = request.TagIds.Select(x => AddPostTags(x, post.Id)).ToList();
                post.PostTag = tags;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return Json(nameof(Index));
            }

            return Json(post);
        }


        /// <summary>
        /// Delete post
        /// </summary>
        [Route("/api/post/delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost([FromBody] Post post)
        {
            var model = await _context.Post.FindAsync(post.Id);
            model.IsActive = 0;
            _context.Post.Update(model);
            await _context.SaveChangesAsync();
            return Json(post);
        }


        /// <summary>
        /// Edit post
        /// </summary>
        [Route("/api/post/edit")]
        [HttpPost]
        public async Task<IActionResult> EditPost([FromBody] PostViewModel request)
        {
            var post = await _context.Post.Include(b => b.PostCategory).Include(c => c.PostTag).Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                post.Title = request.Title;
                post.Content = request.Content;
                post.CreatedDate = DateTime.Now;
                post.UpdatedDate = DateTime.Now;
                post.IsActive = 1;

                List<PostCategory> list = new List<PostCategory>();
                list = request.CategoryIds.Select(x => AddPostCategories(x, request.Id)).ToList();
                post.PostCategory = list;

                List<PostTag> tags = new List<PostTag>();
                tags = request.TagIds.Select(x => AddPostTags(x, request.Id)).ToList();
                post.PostTag = tags;

                _context.Post.Update(post);
                await _context.SaveChangesAsync();
                return Json(nameof(Index));
            }

            return Json(post);
        }

        private PostTag AddPostTags(string x, int postId)
        {
            return new PostTag()
            {
                PostId = postId,
                TagId = int.Parse(x)
            };
        }

        private PostCategory AddPostCategories(string x, int postId)
        {
            return new PostCategory()
            {
                PostId = postId,
                CategoryId = int.Parse(x)
            };
        }


        /// <summary>
        /// Publish post
        /// </summary>
        [Route("/api/post/ispublish")]
        [HttpPost]
        public async Task<IActionResult> PublishPost([FromBody] Post post)
        {
            var model = await _context.Post.FindAsync(post.Id);
            model.IsPublish = post.IsPublish;
            _context.Post.Update(model);
            await _context.SaveChangesAsync();
            return Json(post);
        }


        /// <summary>
        /// Get post by id
        /// </summary>
        [Route("/api/post/getbyid")]
        [HttpPost]
        public async Task<IActionResult> GetPostById([FromBody] Post post)
        {
            var result = await _context.Post.Where(x => x.IsActive == 1).Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                IsActive = x.IsActive,
                IsPublish = x.IsPublish,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Categories = x.PostCategory.Select(y => y.CategoryId).ToList(),
                Tags = x.PostTag.Select(y => y.TagId).ToList(),
                FirstName = "agasg"
                //Categories = x.PostCategory.Select(y => new
                //{
                //    Id = y.CategoryId,
                //    Name = _context.Category.Where(x => x.Id == y.CategoryId).FirstOrDefault().Name
                //}),
                //Tags = x.PostTag.Select(y => new
                //{
                //    Id = y.TagId,
                //    Name = _context.Tag.Where(x => x.Id == y.TagId).FirstOrDefault().Name
                //}),
            }).FirstOrDefaultAsync(y => y.Id == post.Id);

            return Json(result);
        }


    }
}
