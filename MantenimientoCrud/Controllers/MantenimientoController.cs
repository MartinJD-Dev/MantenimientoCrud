﻿using MantenimientoCrud.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace MantenimientoCrud.Controllers
{
    public class MantenimientoController : Controller
    {
        string connectionString;
        DB_AR_AccountDataContext db;

        public MantenimientoController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DB_9E3FAF_CIFASAConnectionString"].ConnectionString;
            db = new DB_AR_AccountDataContext(connection: connectionString);
        }

        [HttpGet]
        public ActionResult Inicio()
        {
            using (var context = new DB_AR_AccountDataContext(connection: connectionString))
            {
                var productos = context.sp_ObtenerCuenta().ToList();
                return View(productos);
            }
        }


        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(AR_Account c)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DB_AR_AccountDataContext(connection: connectionString))
                {
                    context.sp_InsertarCuenta(c.Number, c.Name, c.ExtCode, c.Title, c.FirstName, c.MiddleName, c.LastName,
                        c.Suffix, c.Company, c.JobTitle, c.Address1, c.Address2, c.City, c.State, c.Zip, c.Country,
                        c.PhoneNumber, c.MobileNumber, c.FaxNumber, c.EMail, c.HomePage, c.IMAddress, c.StatementAddress);
                        

                }
            }
            return RedirectToAction("Inicio","Mantenimiento");
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if(id == null)
                return RedirectToAction("Inicio", "Mantenimiento");

            using (var context = new DB_AR_AccountDataContext(connection: connectionString))
            {
                var c = context.AR_Account.FirstOrDefault(x => x.ID == id);

                return View(c);
            }
        }

        [HttpPost]
        public ActionResult Editar(AR_Account c)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DB_AR_AccountDataContext(connection: connectionString))
                {
                    context.sp_ActualizarCuenta(c.ID, c.Name, c.Company, c.City, c.Country, c.PhoneNumber, c.EMail, c.ClosingDate, c.Notes);
                }
            }
            return RedirectToAction("Inicio", "Mantenimiento");
        }

        [HttpPost]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return RedirectToAction("Inicio", "Mantenimiento");

            using (var context = new DB_AR_AccountDataContext(connection: connectionString))
            {
                context.sp_EliminarCuenta(id);
            }
            return RedirectToAction("Inicio", "Mantenimiento");
        }
    }
}