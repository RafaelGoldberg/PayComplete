using System;
using System.Collections.Generic;
using System.Text;

namespace PayComplete.Services.Implementation
{
    class SocailSecurityContributionService : ISocialSecurityContributionService
    {
        private decimal SSRate;
        private decimal SSC;
        public decimal SSContribution(decimal totalAmount)
        {
            if (totalAmount < 819)
            {
                //lower Earning Limit
                SSRate = .0m;
                SSC = 0;
            }
            else if (totalAmount >= 819 && totalAmount <= 4167)
            {
                SSRate = .12m;
                SSC = ((totalAmount - 819) * SSRate);
            }
            else if (totalAmount > 4167)
            {
                SSRate = .02m;
                SSC = ((4167 - 819) * .12m) + ((totalAmount - 4167) * SSC);
            }
            return SSC;
        }
    }
}
