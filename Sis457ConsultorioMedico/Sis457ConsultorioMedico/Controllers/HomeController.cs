using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sis457ConsultorioMedico.Models;

namespace Sis457ConsultorioMedico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


