﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PayComplete.Services
{
    public interface ITaxService
    {
        decimal TaxAmount(decimal totalAmount);
    }
}
