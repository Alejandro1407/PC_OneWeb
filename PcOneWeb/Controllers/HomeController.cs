using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PcOneWeb.Servicios;
using System.Collections.Generic;


namespace PcOneWeb.Controllers
{
    public class HomeController : Controller
    {
        Servicio_Productos servicio_productos = new Servicio_Productos();
        SesionServicio sesionServicio = new SesionServicio();
        // GET: Home
        public ActionResult Index()
        {
           return View();
        }
      
        public async Task<PartialViewResult> MostrarCategorias()
        {
            List<Categoria> Model = await servicio_productos.ObtenerCategoriasAsync();
            return PartialView("_ParcialViewIndex", Model);
        }
        public async Task<ActionResult> Catalogo()
        {
            var model = await servicio_productos.ObtenerCategoriasAsync();
            return View(model);
        }

        public PartialViewResult MostrarDatos(int Categoria = 0, string Parametro = "", bool IsSearch = false)
        {
            ViewBag.IsSearch = IsSearch;
            List<Producto> Model =  servicio_productos.ObtenerProductos(Categoria, Parametro);
            return PartialView("_ParcialViewCatalogo",Model);
        }
        [HttpPost]
        public bool CheckEmail(string email)
        {
            return sesionServicio.CheckEmail(email);
            
        }
        [HttpPost]
        public bool RegistrarUsuario(string email,string contrasena,string usuario,string telefono)
        {
            Cliente c = new Cliente();
            c.email = email;
            c.nombre = usuario;
            c.telefono = telefono;
            c.contrasena = contrasena;
            return sesionServicio.RegistarUsuario(c); 
        }
        [HttpPost]
        public string ComprobarLogin(string email = "",string contrasena = "")
        {
            return sesionServicio.ComprobarLogin(email,contrasena);
        }
        [HttpPost]
        public PartialViewResult ModalProducto(int param)
        {
            var model = servicio_productos.ObtenerProducto(param);
            return PartialView("_ParcialViewModal",model);
        }


    }//Clase
}//NameSpace


