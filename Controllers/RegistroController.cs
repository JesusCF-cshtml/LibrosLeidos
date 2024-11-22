using Libros_Leidos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Libros_Leidos.Controllers
{
    public class RegistroController : Controller
    {
        // GET: RegistroController
        public ActionResult Index()
        {
            // Obtener todos los registros
            return View(new Registros().GetRegistros());
        }

        // GET: RegistroController/Details/5
        public ActionResult Details(int id)
        {
            var registros = new Registros();
            var resultado = registros.GetRegistros().FirstOrDefault(r => r.id_usuario == id && r.id_libro == id); // Modifica esta lógica según tus requisitos
            return View(resultado);
        }

        // GET: RegistroController/Create
        public ActionResult Create()
        {
            // Aquí puedes cargar los datos necesarios, como listas de libros y usuarios
            return View();
        }

        // POST: RegistroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Registros registro)
        {
            try
            {
                registro.AddRegistro(registro); // Asegúrate de implementar el método AddRegistro en la clase Registros
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegistroController/Edit/5
        public ActionResult Edit(int id)
        {
            var registros = new Registros();
            var resultado = registros.GetRegistros().FirstOrDefault(r => r.id_usuario == id && r.id_libro == id); // Modifica esta lógica según tus requisitos
            return View(resultado);
        }

        // POST: RegistroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Registros registro)
        {
            try
            {
                registro.EditRegistro(registro); // Asegúrate de implementar el método EditRegistro en la clase Registros
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegistroController/Delete/5
        public ActionResult Delete(int id)
        {
            var registros = new Registros();
            var resultado = registros.GetRegistros().FirstOrDefault(r => r.id_usuario == id && r.id_libro == id); // Modifica esta lógica según tus requisitos
            return View(resultado);
        }

        // POST: RegistroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var registros = new Registros { id_usuario = id }; // Cambia esta lógica según el caso
                registros.DeleteRegistro(registros); // Asegúrate de implementar el método DeleteRegistro en la clase Registros
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

