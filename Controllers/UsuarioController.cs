using Libros_Leidos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libros_Leidos.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public ActionResult Index()
        {
            return View(new Usuarios().GetUsuarios());
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Usuarios usu)
        {
            try
            {
                usu.AddUsuario(usu);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            var usuarios = new Usuarios();
            var resultado = usuarios.GetUsuarios().FirstOrDefault(p => p.id_usuario == id);//Obtener la lista de personas, buscar un objeto donde coindida 
            return View(resultado);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Usuarios usu)
        {
            try
            {
                usu.EditUsuario(usu);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            var usuarios = new Usuarios();
            var resultado = usuarios.GetUsuarios().FirstOrDefault(p => p.id_usuario == id);
            return View(resultado);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var usuarios = new Usuarios { id_usuario = id };
                usuarios.DeleteUsuario(usuarios);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
