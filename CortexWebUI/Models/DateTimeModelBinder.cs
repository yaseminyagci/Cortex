using Cortex.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cortex.WebUI.Models
{
    public class DateTimeModelBinder:DefaultModelBinder
    {   //Amaç date time gibi specific bindingler için int bişey var property e binging etmek istiyoruz bunu burda yapabiliriz
        //
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cultureInfo = controllerContext.HttpContext.Request.Cookies["CultureInfo"];
            var currentCulcure = "tr-TR";
            if (cultureInfo != null)
                currentCulcure = cultureInfo.Value;

            DateTime dateTime;

            var valueProvider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProvider == null)
                return null;

            if (valueProvider.AttemptedValue.IsNull())
                return null;
            
            //var parseResult = DateTime.TryParse(valueProvider.AttemptedValue, new CultureInfo(currentCulcure), DateTimeStyles.None, out dateTime);
            if (DateTime.TryParse(valueProvider.AttemptedValue, new CultureInfo(currentCulcure), DateTimeStyles.None, out dateTime))
                return dateTime;

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid Date");
            return null;
        }
    }
}