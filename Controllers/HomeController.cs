using AplicacionWEBMVCAbril.Datos;
using AplicacionWEBMVCAbril.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AplicacionWEBMVCAbril.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public HomeController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Usuario.ToListAsync()) ;
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        //metodo para crear nuevo usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Crear(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contexto.Usuario.Add(usuario);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El usuario se creó correctamente";
                return RedirectToAction("Index");
            }
            return View();
        }
        //cargar vista para editar
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _contexto.Usuario.Find(id);
            if (usuario == null)
            { 
                return NotFound(); 
            }
            return View(usuario);
         }

        //metodo post para guardar cambios en la edicion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(usuario);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El usuario se editó correctamente";
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
        //metodo para cargar vista en borrar
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _contexto.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }


        //metodo para guardar el borrado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarRegistro(int? id)
        {
          var usuario = await _contexto.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _contexto.Usuario.Remove(usuario);
                await _contexto.SaveChangesAsync();
            TempData["Mensaje"] = "El usuario se eliminó correctamente";
            return RedirectToAction("Index");
      
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}