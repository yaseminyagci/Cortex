using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortex.Domain
{
  public  interface IModel:IDomain
    {
         int Id { get; set; }
    }
}
