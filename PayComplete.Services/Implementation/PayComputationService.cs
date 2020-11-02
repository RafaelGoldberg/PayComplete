using Microsoft.AspNetCore.Mvc.Rendering;
using PayComplete.Entity;
using PayComplete.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayComplete.Services.Implementation
{
    public class PayComputationService : IPayComputationService
    {
        private readonly ApplicationDbContext _dbContext;
        private decimal contractualEarnings;
        private decimal overtimeHours;

        public PayComputationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
            {
                contractualEarnings = hoursWorked * hourlyRate;
            }
            else
            {
                contractualEarnings = contractualHours * hourlyRate;
            }
            return contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _dbContext.PaymentRecords.AddAsync(paymentRecord);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll() => _dbContext.PaymentRecords.OrderBy(p => p.EmployeeId);

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYears = _dbContext.TaxYears.Select(taxYears => new SelectListItem
            {
                Text = taxYears.YearOfTax,
                Value = taxYears.Id.ToString()
            });
            return allTaxYears;
        }

        public PaymentRecord GetById(int id) => _dbContext.PaymentRecords.Where(pay => pay.Id == id).FirstOrDefault();

        public TaxYear GetTaxYearById(int id) => _dbContext.TaxYears.Where(year => year.Id == id).FirstOrDefault();


        public decimal NetPay(decimal totalEarnings, decimal totalDeductions) => totalEarnings - totalDeductions;

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours) => overtimeHours * overtimeRate;

        public decimal OverTimeHours(decimal hoursWorked, decimal contactualHours)
        {
            if (hoursWorked <= contactualHours)
            {
                overtimeHours = 0.00m;
            }
            else if (hoursWorked > contactualHours)
            {
                overtimeHours = hoursWorked - contactualHours;
            }
            return overtimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate) => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal ssc, decimal studentLoanRepayment, decimal unionFee) 
            => tax + ssc + studentLoanRepayment + unionFee;

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings) => overtimeHours + contractualEarnings;
    }
}
