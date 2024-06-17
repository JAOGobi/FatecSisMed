using FatecSisMed.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FatecSisMed.Web.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaViewModel>>> Index()
        {
            var result = await _marcaService.GetAllMarcas();
            if (result is null) return View("Error");
            return View(result);
        }

        // criar a view CreateMarca
        [HttpGet]
        public async Task<IActionResult> CreateMarca()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            CreateMarca(MarcaViewModel marcaViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await
                    _marcaService.CreateMarca(marcaViewModel);
                if (result is not null) return RedirectToAction(nameof(Index));
            }
            else
                return BadRequest("Error");

            return View(marcaViewModel);
        }

        // Criar a view UpdateMarca
        [HttpGet]
        public async Task<IActionResult> UpdateMarca(int id)
        {
            var result = await _marcaService.FindMarcaById(id);
            if (result is null) return View("Error");
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult>
            UpdateMarca(MarcaViewModel marcaViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await
                    _marcaService.UpdateMarca(marcaViewModel);
                if (result is not null) return RedirectToAction(nameof(Index));
            }

            return View(marcaViewModel);
        }

        // criar a view delete marca
        [HttpGet]
        public async Task<ActionResult<MarcaViewModel>>
            DeleteMarca(int id)
        {
            var result = await _marcaService.FindMarcaById(id);
            if (result is null) return View("Error");
            return View(result);
        }

        [HttpPost(), ActionName("DeleteMarca")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _marcaService.DeleteMarcaById(id);
            if (!result) return View("Error");
            return RedirectToAction("Index");
        }
    }
}

