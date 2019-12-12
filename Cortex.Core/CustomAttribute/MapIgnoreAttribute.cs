using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortex.Core.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
   public class MapIgnoreAttribute : Attribute
    {
    }
}
