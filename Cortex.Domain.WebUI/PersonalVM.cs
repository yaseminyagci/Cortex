using Cortex.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortex.Domain.WebUI
{
   public class PersonalVM : IModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string task { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
