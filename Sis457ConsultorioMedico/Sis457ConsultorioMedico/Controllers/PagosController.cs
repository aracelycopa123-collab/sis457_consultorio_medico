using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis457ConsultorioMedico.Models;

namespace Sis457ConsultorioMedico.Controllers
{
    public class PagosController : Controller
    {
        private readonly FinalConsultorioMedicoContext _context;

        public PagosController(FinalConsultorioMedicoContext context)
        {
            _context = context;
        }

