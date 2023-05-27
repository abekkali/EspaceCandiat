using EspaceCandiat.Data;
using EspaceCandiat.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspaceCandiat.Controllers
{
    public class PublicController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public PublicController(IWebHostEnvironment environment, ApplicationDbContext context,IEmailSender emailSender)
        {
            _environment = environment;
            _context = context;
            _emailSender = emailSender;
        }
        [HttpPost]
        public async Task<IActionResult> AjouterCand(Candidat candidat, IFormFile cv)
        {
            try
            {
                var folderName = Path.Combine("Candidatures", candidat.Nom + "_" + candidat.Prenom);
                var pathToSave = Path.Combine(_environment.WebRootPath, folderName);
                Directory.CreateDirectory(pathToSave);

                var fileName = Path.GetFileName(cv.FileName);
                var fullPath = Path.Combine(pathToSave, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await cv.CopyToAsync(stream);
                }

                candidat.Cv = fullPath;
                _context.Candidats.Add(candidat);
                await _context.SaveChangesAsync();

                var subject = "Confirmation de candidature";
                var message = $"Bonjour {candidat.Nom}, vous avez postulé avec succès pour l'offre )";
                //await _emailSender.SendEmailAsync(candidat.Email, subject, message);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
    }
}
