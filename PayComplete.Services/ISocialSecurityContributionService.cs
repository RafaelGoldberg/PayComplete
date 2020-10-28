using System;
using System.Collections.Generic;
using System.Text;

namespace PayComplete.Services
{
    public interface ISocialSecurityContributionService
    {
        decimal SSContribution(decimal totalAmount);
    }
}
