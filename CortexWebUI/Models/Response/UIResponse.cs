using Cortex.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cortex.WebUI.Models.Response
{
    public class UIResponse
    {
        public string Message { get; set; }

        public object Data { get; set; }

        public OperationResultType ResultType { get; set; }
    }
}