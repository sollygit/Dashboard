using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Models.Warnings
{
    public class WarningTester
    {
        private IEnumerable<IWarning> warnings;

        public WarningTester(IEnumerable<IWarning> warnings)
        {
            this.warnings = warnings;
        }

        public IEnumerable<string> Test(DeliveryOrder order)
        {
            return warnings.Select(w => w.Test(order)).Where(w => w != null);
        }
    }
}