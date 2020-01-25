using Microsoft.AspNetCore.Mvc;
using RedeSocialMVC.Models;
using RedeSocialMVC.Repository;

namespace RedeSocialMVC.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioRepository repository = new UsuarioRepository();

        // GET: Usuario
        public ActionResult Index()
        {            
            var usuarios = repository.ListarUsuarios();
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuario = repository.DetalharUsuario(id);

            if (usuario == null)
            {
                return StatusCode(404);
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.CriarUsuario(usuario);
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = repository.DetalharUsuario(id);

            if (usuario == null)
            {
                return StatusCode(404);
            }

            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                repository.AtualizarUsuario(usuario);
                return RedirectToAction("Index");
            }
            else
            {
                return View(usuario);
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = repository.DetalharUsuario(id);

            if (usuario == null)
            {
                return StatusCode(404);
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Usuario usuario)
        {
            repository.ExcluirUsuario(usuario.Id);
            return RedirectToAction("Index");
        }
    }
}