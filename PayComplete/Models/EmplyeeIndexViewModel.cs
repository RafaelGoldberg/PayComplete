using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayComplete.Models
{
    public class EmplyeeIndexViewModel
    {
        public int Id { get; set;}
        public string EmployeeNo { get; set; }
        public string  FullName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateJoined { get; set; }
        public string Designation { get; set; }
        public string City { get; set; }
    }
}
