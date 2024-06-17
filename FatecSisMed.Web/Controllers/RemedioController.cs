using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FatecSisMed.Web.Controllers
{
    public class RemedioController : Controller
    {

        private readonly IRemedioService _remedioService;
        private readonly IConvenioService _convenioService;
        private readonly IEspecialidadeService _especialidadeService;

        public RemedioController(IRemedioService remedioService, IConvenioService convenioService, IEspecialidadeService especialidadeService)
        {
            _remedioService = remedioService;
            _convenioService = convenioService;
            _especialidadeService = especialidadeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemedioViewModel>>> Index()
        {
            var result = await _remedioService.GetAllRemedios();
            if (result is null) return View("Error");
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> CreateRemedio()
        {
            ViewBag.EspecialidadeId = new SelectList(await _especialidadeService.GetAllEspecialidades(), "Id", "Nome");
            ViewBag.ConvenioId = new SelectList(await _convenioService.GetAllConvenios(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRemedio(RemedioViewModel remedioViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _remedioService.CreateRemedio(remedioViewModel);
                if (result != null) return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.EspecialidadeId = new SelectList(await
                                     _especialidadeService.GetAllEspecialidades(), "Id", "Nome");
                ViewBag.BrandId = new SelectList(await
                                     _convenioService.GetAllConvenios(), "Id", "Nome");
            }

            return View(remedioViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRemedio(int id)
        {

            ViewBag.EspecialidadeId = new
            SelectList(await _especialidadeService.GetAllEspecialidades(), "Id", "Nome");
            ViewBag.ConvenioId = new
            SelectList(await _convenioService.GetAllConvenios(), "Id", "Nome");

            var result = await _remedioService.FindRemedioById(id);

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRemedio(RemedioViewModel remedioViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _remedioService.UpdateRemedio(remedioViewModel);
                if (result is not null) return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest("Error");
            }

            return View(remedioViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<RemedioViewModel>> DeleteRemedio(int id)
        {
            var result = await _remedioService.FindRemedioById(id);
            if (result is null) return View("Error");
            return View(result);
        }

        [HttpPost(), ActionName("DeleteRemedio")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _remedioService.DeleteRemedioById(id);
            if (!result) return View("Error");
            return RedirectToAction("Index");
        }

    }
}

