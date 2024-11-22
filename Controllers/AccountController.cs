using Microsoft.AspNetCore.Mvc;
using Libros_Leidos.Models;

namespace Libros_Leidos.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string contraseña)
        {
            // Validar las credenciales
            if (usuario == "admin" && contraseña == "1234")
            {
                // Redirigir al usuario a la página principal o al Dashboard
                return RedirectToAction("Index", "Home");
            }

            // Si las credenciales son incorrectas, mostrar un mensaje de error
            ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
            return View();
        }
    }
}
