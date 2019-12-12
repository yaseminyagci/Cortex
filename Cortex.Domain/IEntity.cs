using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortex.Domain
{
    public interface IEntity:IDomain
    {
        [Key]
        int Id { get; set; }

        DateTime CreateDate { get; set; }
    }
}
