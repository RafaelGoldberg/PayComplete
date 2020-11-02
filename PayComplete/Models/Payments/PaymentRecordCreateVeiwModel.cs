using PayComplete.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayComplete.Models.Payments
{
    public class PaymentRecordCreateVeiwModel
    {
        public int Id { get; set; }

        [Display(Name =("Full Name"))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [MaxLength(150)]
        public string FullName { get; set; }
        public string SSN { get; set; }

        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Pay Month")]
        public string PayMonth { get; set; } = DateTime.Today.Month.ToString();
        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }
        public string TaxCode { get; set; } = "1230L";
        [Display(Name =  "Hourly Rate")]
        public decimal HourlyRate { get; set; }
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }
        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; } = 40m;

        public decimal OvertimeHours { get; set; }

        public decimal ContractualEarnings { get; set; }

        public decimal OvertimeEarnings { get; set; }

        public decimal Tax { get; set; }

        public decimal SSC { get; set; }

        public decimal? UnionFee { get; set; }

        public decimal? SLC { get; set; } // Student Loan Collection

        public decimal TotalEarnings { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetPayment { get; set; }
    }
}
