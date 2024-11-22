using Libros_Leidos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libros_Leidos.Controllers
{
    public class LibroController : Controller
    {
        // GET: LibroController
        public ActionResult Index()
        {
            return View(new Libros().GetLibros());
        }

        // GET: LibroController/Details/5
        public ActionResult Details(int id)
        {
            var libros = new Libros();
            var resultado = libros.GetLibros().FirstOrDefault(l => l.id_libro == id);
            return View(resultado);
        }

        // GET: LibroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Libros libro)
        {
            try
            {
                libro.AddLibro(libro);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibroController/Edit/5
        public ActionResult Edit(int id)
        {
            var libros = new Libros();
            var resultado = libros.GetLibros().FirstOrDefault(l => l.id_libro == id);
            return View(resultado);
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Libros libro)
        {
            try
            {
                libro.EditLibro(libro);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibroController/Delete/5
        public ActionResult Delete(int id)
        {
            var libros = new Libros();
            var resultado = libros.GetLibros().FirstOrDefault(l => l.id_libro == id);
            return View(resultado);
        }

        // POST: LibroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var libros = new Libros { id_libro = id };
                libros.DeleteLibro(libros);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}