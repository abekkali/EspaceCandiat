using EspaceCandiat.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EspaceCandiat.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> index()
        {
            return View(await _context.Candidats.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var candidat = await _context.Candidats.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }

            _context.Candidats.Remove(candidat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("/Admin/GetCV/{id}")]
        public async Task<IActionResult> GetCV(int id)
        {
            var candidat = await _context.Candidats.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }

            var cvPath = candidat.Cv;

            var extension = Path.GetExtension(cvPath).ToLower();
            var contentType = "application/pdf"; // Par défaut sinon 

            if (extension == ".jpg" || extension == ".jpeg")
            {
                contentType = "image/jpeg";
            }
            else if (extension == ".png")
            {
                contentType = "image/png";
            }

            return PhysicalFile(cvPath, contentType);
        }



    }
}
