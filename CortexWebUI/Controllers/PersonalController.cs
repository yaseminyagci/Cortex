using Cortex.Domain.WebUI;
using Cortex.Service;
using Cortex.Core;
using Cortex.WebUI.Models.DataTableResponse;
using Cortex.WebUI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CortexWebUI.Controllers;
namespace CortexWebUI.Controllers
{
    public class PersonalController:ControllerBase
    {
        readonly private PersonalService _PersonalService;
        // GET: Personal
        public PersonalController()
        {
            _PersonalService = new PersonalService();

        }
        public ActionResult PersonalList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PersonalAdd()
        {
            return View(new PersonalVM());
        }

        [HttpGet]
        public ActionResult PersonalEdit(int id)
        {
            var model = _PersonalService.GetPersonalId(id);
            if (!model.IsSuccess)
            {
                RedirectToAction(nameof(PersonalList));
            }

            return View(model.Data);
        }

        public JsonResult GetPersonalList()
        {
            var filteredData = _PersonalService.GetPersonals();
            var gridPageRecord = filteredData.Data.Skip(0).Take(50).ToList();

            DataTablesResponse tableResult = new DataTablesResponse(1, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePersonal(PersonalVM model)
        {
            if (!ModelState.IsValid)
            {
                return base.JSonModelStateHandle();
            }

            ServiceResultModel<PersonalVM> serviceResult = _PersonalService.SavePersonal(model);

            if (!serviceResult.IsSuccess)
            {
                base.UIResponse = new UIResponse
                {
                    Message = string.Format("Operation Is Not Completed, {0}", serviceResult.Message),
                    ResultType = serviceResult.ResultType,
                    Data = serviceResult.Data
                };
            }
            else
            {
                base.UIResponse = new UIResponse
                {
                    Data = serviceResult.Data,
                    ResultType = serviceResult.ResultType,
                    Message = "Success"
                };
            }

            return Json(base.UIResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdatePersonal(PersonalVM model)
        {
            if (model.Id <= 0)
            {
                RedirectToAction(nameof(PersonalList)); // ErrorHandle eklenecek
            }

            if (!ModelState.IsValid)
            {
                return base.JSonModelStateHandle();
            }

            ServiceResultModel<PersonalVM> serviceResult = _PersonalService.UpdateHotelType(model);

            if (!serviceResult.IsSuccess)
            {
                base.UIResponse = new UIResponse
                {
                    Message = string.Format("Operation Is Not Completed, {0}", serviceResult.Message),
                    ResultType = serviceResult.ResultType,
                    Data = serviceResult.Data
                };
            }
            else
            {
                base.UIResponse = new UIResponse
                {
                    Data = serviceResult.Data,
                    ResultType = serviceResult.ResultType,
                    Message = "Success"
                };
            }

            return Json(base.UIResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePersonal(int id)
        {
            if (id <= 0)
            {
                return Json(base.UIResponse = new UIResponse
                {
                    ResultType = Cortex.Core.OperationResultType.Error,
                    Message = string.Format("id is not valid, this Id = {0}", id)
                }, JsonRequestBehavior.AllowGet);
            }

            ServiceResultModel<PersonalVM> serviceResult = _PersonalService.DeletePersonal(id);
            return Json(base.UIResponse = new UIResponse
            {
                ResultType = serviceResult.ResultType,
                Data = serviceResult.Data,
                Message = serviceResult.ResultType == OperationResultType.Success ? "Record Deleted Successfully" : string.Format("Warning.. {0}", serviceResult.Message)
            });
        }

     
    }
}