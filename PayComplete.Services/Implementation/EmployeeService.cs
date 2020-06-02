using PayComplete.Entity;
using PayComplete.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayComplete.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;
        public EmployeeService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Employee newEmployee)
        {
            await _db.Employees.AddAsync(newEmployee);
            await _db.SaveChangesAsync();
        }

        public Employee GetById(int employeeId) => _db.Employees.Where(e => e.Id == employeeId).FirstOrDefault();

        public async Task Delete(int employeeId)
        {
            var employee = GetById(employeeId);
            _db.Remove(employee);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll() => _db.Employees;


        public async Task UpdateAsync(Employee employee)
        {
            _db.Update(employee);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int employeeId)
        {
            var employee = GetById(employeeId);
            _db.Update(employee);
            await _db.SaveChangesAsync();
        }

        public decimal UnionFees(int id)
        {
            throw new NotImplementedException();
        }


        public decimal StudentLoanRepaymentAmount(int id, decimal totalAmount)
        {
            throw new NotImplementedException();
        }

      

    }
}
