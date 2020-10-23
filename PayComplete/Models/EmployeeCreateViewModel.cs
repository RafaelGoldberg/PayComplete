using Microsoft.AspNetCore.Http;
using PayComplete.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayComplete.Models
{
    public class EmployeeCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee Number is required"),
         RegularExpression(@"^[A-Z]{3,3}[0-9]{3}$")]
        public string EmployeeNo { get; set; }
        [Required(ErrorMessage = "First Name is required"), StringLength(50, MinimumLength = 2), Display(Name = "First Name")]
        //[RegularExpression(@"^[A-Z][a-zA-Z""'\s-]x$")]
        public string FirstName { get; set; }
        [MaxLength(50), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "First Name is required"), StringLength(50, MinimumLength = 2), Display(Name = "Last Name")]
        //[RegularExpression(@"^[A-Z][a-zA-Z""'\s-]x$")]
        public string LastName { get; set; }
        public string FullName { get
            {
                return FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0]).ToUpper()) + LastName;
            }
        }
        public string Gender { get; set; }
        [Display(Name ="Photo")]
        public IFormFile ImageUrl { get; set; }
       [DataType(DataType.Date), Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Job Role is required")]
        public string Designation { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Required, MaxLength(25),Display(Name = "Social Security Number")]
       // [RegularExpression(@"^\d{3}-\d{2}-\d{4}$")]
        public string SocialSecurityNumber { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Student Loan")]
        public StudentLoan StudentLoan { get; set; }
        [Display(Name = "Union Member")]
        public UnionMember UnionMember { get; set; }
        [Required, StringLength(150)]
        public string Address { get; set; }
        [Required, StringLength(50)]
        public string City { get; set; }

        [Required, StringLength(30)]
        public string State { get; set; }

        [Required, StringLength(50), Display(Name ="Zip Code")]
        public string ZipCode { get; set; }

       //public IEnumerable<PaymentRecord> PaymentRecords { get; set; }
    }
}
