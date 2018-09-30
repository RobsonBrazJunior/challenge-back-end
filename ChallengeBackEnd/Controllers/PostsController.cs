using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChallengeBackEnd.Data;
using ChallengeBackEnd.Models;

namespace ChallengeBackEnd.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var postsViewModel = new PostsViewModel()
            {
                 Posts = await _context.Posts.OrderByDescending(p => p.PostID).OrderByDescending(p => p.Likes).ToListAsync()
            };
            foreach(var post in postsViewModel.Posts)
            {
                post.PercentualLikesSobreTotal = (post.Likes / (double)postsViewModel.TotalDeLikes.Value) * 100;
                post.PercentualViewsSobreTotal = (post.Views / (double)postsViewModel.TotalDeViews.Value) * 100;
            }

            return View(postsViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await CarregarPost(id.Value);

            if (post == null)
                return NotFound();

            await ContabilizarViewsAsync(post);

            return View(post);
        }

        private async Task ContabilizarViewsAsync(Post post)
        {
            post.Views += 1;
            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> CountLike(int id)
        {
            var post = await CarregarPost(id);
            post.Likes += 1;
            _context.Update(post);
            await _context.SaveChangesAsync();
            return View("Details", post);
        }

        private async Task<Post> CarregarPost(int id) =>
            await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);              

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,Titulo,Resumo,Conteudo")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostID,Titulo,Resumo,Conteudo")] Post post)
        {
            if (id != post.PostID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id) => _context.Posts.Any(e => e.PostID == id);
    }
}
