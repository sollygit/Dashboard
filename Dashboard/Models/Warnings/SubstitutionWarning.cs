using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models;

namespace Dashboard.Models.Warnings
{
    public class SubstitutionWarning : IWarning
    {
        public string Test(DeliveryOrder order)
        {
            return order.Lines != null && order.Lines.Any(l => l.Substitution) ? "Substitution" : null;
        }
    }
}
