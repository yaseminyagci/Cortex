using Cortex.Data.Context;
using Cortex.Domain.Entity;
using Cortex.Service;
using Cortex.WebUI.Models.DataTableResponse;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CortexWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonalService _PersonalService;

        public HomeController()
        {
            _PersonalService = new PersonalService();

        }
        // GET: HotelDefinition
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StokEkstre()
        {
            return View();
        }
        public JsonResult GetStokDefinitionList()
        {
            var filteredData = _PersonalService.GetPersonals();
            var gridPageRecord = filteredData.Data.Skip(0).Take(50).ToList();

            DataTablesResponse tableResult = new DataTablesResponse(1, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

    }
}