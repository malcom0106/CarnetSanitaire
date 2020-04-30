using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using CarnetSanitaire.Web.UI.Data;

namespace CarnetSanitaire.Web.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DataEtablissement _dataEtablissement;

        public HomeController(DataEtablissement dataEtablissement)
        {
            _dataEtablissement = dataEtablissement;
        }

        public async Task<IActionResult> Index()
        {
            Etablissement etablissement = await _dataEtablissement.GetEtablissementByUser();
            return View(etablissement);
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
    }
}
