using System;
using System.Collections.Generic;
using System.Text;

namespace PayComplete.Services.Implementation
{
    public class TaxService : ITaxService
    {
        private decimal taxRate;
        private decimal tax;
        private decimal TaxFreeRate = .0m;
        private decimal FirstBracketTaxRate = .20m;
        private decimal SecondBracketTaxRate = .40m;
        private decimal ThirdBracketTaxRate = .40m;
        public decimal TaxAmount(decimal totalAmount)
        {
            if (totalAmount < 1500)
            {
                taxRate = TaxFreeRate; //No Tax
                tax = totalAmount * taxRate;
            }
            else if (totalAmount > 1500 && totalAmount <= 3000)
            {
                taxRate = FirstBracketTaxRate;

                tax = ((totalAmount - 1500) * taxRate);
            }
            else if (totalAmount > 3000 && totalAmount <= 12300)
            {
                //higher tax rate
                taxRate = SecondBracketTaxRate;
                tax = (1500 * FirstBracketTaxRate) + ((totalAmount - 3000) * SecondBracketTaxRate);
            }
            else if (totalAmount > 123000)
            {
                tax = (1500 * FirstBracketTaxRate) + ((12300 - 3000) * SecondBracketTaxRate) + ((totalAmount - 12300) * ThirdBracketTaxRate);
            }
            return tax;
        }
    }
}
