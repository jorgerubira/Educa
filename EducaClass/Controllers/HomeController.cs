using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EducaClass.Models;
using Microsoft.EntityFrameworkCore;
using EducaClass.Services;

namespace EducaClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult TestConexion()
        {
            //Estas variables las debe enviar el usuario en un formulario.
            string server = "localhost";
            string user = "root";
            string password = "1111";

            //Conexión dinámica a la base de datos.
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseMySql($"Server={server};Uid={user};pwd={password};");
            Contexto ctx = new Contexto(optionsBuilder.Options);
            SQLService sql = new SQLService();
            List<MyModelGeneral> resultado = sql
                                        .EjecutarSQL<MyModelGeneral>(ctx, "SHOW DATABASES;",
                                            x => new MyModelGeneral()
                                            {
                                                Database = x.GetString(0)
                                            });
            return View(resultado);
        }
    }
}
