using MantenimientoCrud.Models;
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
        public ActionResult Inicio(int pageNumber = 1, int pageSize = 10)
        {
            using (var context = new DB_AR_AccountDataContext(connection: connectionString))
            {
                int totalaccount = context.sp_ObtenerCuenta().Count();

                int totalPages = (int)Math.Ceiling((double)totalaccount / pageSize);

                var productos = context.sp_ObtenerCuenta().
                                        OrderBy(c => c.ID).
                                        Skip((pageNumber - 1) * pageSize).
                                        Take(pageSize).ToList();

                var model = new PaginatedLists<sp_ObtenerCuentaResult>(productos, totalaccount, pageNumber, pageSize);

                return View(model);
            }
        }

    }
}