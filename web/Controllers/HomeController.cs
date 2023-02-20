using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using web.Models;
using web.ViewModels;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MolinoContext _dbcontext;

        public HomeController(MolinoContext context)
        {
            _dbcontext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Index([FromBody] ContratoCrearViewModel objContratoCrearViewModel)
        //{
        //    Contrato contrato = objContratoCrearViewModel.Contrato;
        //    contrato.ValorIndicadorContrato = (ICollection<ValorIndicadorContrato>)objContratoCrearViewModel.Indicadores;
        //    _dbcontext.Contrato.Add(contrato);
        //    _dbcontext.SaveChanges();

        //    return Json(new {respuesta = true});
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}