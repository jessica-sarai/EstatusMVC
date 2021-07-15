using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EstatusMVC.Models;

namespace EstatusMVC.Controllers
{
    public class RazorADOController : Controller
    {
        ADOEstatusAlumno ADOestatus = new ADOEstatusAlumno();
        // GET: RazorADO
        public ActionResult Index()
        {
            List<EstatusAlumno> lstEstatus = ADOestatus.consultarLista();
            return View(lstEstatus);

        }

        public ActionResult Consultar(int id)
        {
            List<EstatusAlumno> lstEstatus = ADOestatus.consultarListaUno(id);
            return View(lstEstatus);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(string clave, string nombre)
        {
            //if (ModelState.IsValid)
            //{
                ADOestatus.agregarLista(clave, nombre);
                Response.Redirect($"Index", false);
              //  return RedirectToAction("Index", "RazorADO");
               
            //}
            return View();
        }

        [HttpGet]
        public ActionResult Actualizar(int id, string clave, string nombre)
        {
            EstatusAlumno estatus = new EstatusAlumno();
            estatus.id = id;
            estatus.clave = clave;
            estatus.nombre = nombre;
            return View(estatus);
        }

        [HttpPost]
        public ActionResult Actualizar(EstatusAlumno estatus)
        {
            if (ModelState.IsValid)
            {
                ADOestatus.actualizarEstatus(estatus.id, estatus.clave, estatus.nombre);
                return RedirectToAction("Index", "RazorADO");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Eliminar(int id, string clave, string nombre)
        {
            EstatusAlumno estatus = new EstatusAlumno();
            estatus.id = id;
            estatus.clave = clave;
            estatus.nombre = nombre;
            return View(estatus);
        }

        [HttpPost]
        public ActionResult Eliminar(EstatusAlumno estatus)
        {
            if (ModelState.IsValid)
            {
                ADOestatus.eliminarEstatus(estatus.id);
                return RedirectToAction("Index", "RazorADO");
            }
            return View();

        }



    }
}