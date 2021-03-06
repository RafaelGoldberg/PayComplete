﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using PayComplete.Entity;
using PayComplete.Models;
using PayComplete.Services;
using PayComplete.Services.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayComplete.Controllers
{

    [Authorize(Roles ="Admin, Manager, Staff")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int? pageNumber)
        {
            var employees = _employeeService.GetAll().Select(employee => new EmplyeeIndexViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                ImageUrl = employee.ImageUrl,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Designation = employee.Designation,
                City = employee.City,
                DateJoined = employee.DateJoined
            }).ToList();
            int pageSize = 4;

            return View(EmployeeListPagination<EmplyeeIndexViewModel>.Create(employees, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task< IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    SocialSecurityNumber = model.SocialSecurityNumber,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    PhoneNumber = model.PhoneNumber,
                    ZipCode =model.ZipCode,
                    Designation = model.Designation,
                };

                if (model.ImageUrl != null && model.ImageUrl.Length >0)
                {
                    var uplaodDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);

                    var webRootPath = _webHostEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmss") + fileName + extension;
                    var path = Path.Combine(webRootPath, uplaodDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                    employee.ImageUrl = "/" + uplaodDir + "/" + fileName;
                }

                await _employeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
               // FullName = employee.FullName,
                DateJoined = employee.DateJoined,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,

                SocialSecurityNumber = employee.SocialSecurityNumber,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                State = employee.State,
                PhoneNumber = employee.PhoneNumber,
                ZipCode = employee.ZipCode,
                Designation = employee.Designation,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);
                if (employee == null)
                {
                    return NotFound();
                }
                employee.EmployeeNo = model.EmployeeNo;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
                employee.FullName = model.FullName;
                employee.SocialSecurityNumber = model.SocialSecurityNumber;
                employee.Gender = model.Gender;
                employee.DateJoined = model.DateJoined;
                employee.Email = model.Email;
                employee.DOB = model.DOB;
                employee.Designation = model.Designation;
                employee.PhoneNumber = model.PhoneNumber;
                employee.PaymentMethod = model.PaymentMethod;
                employee.StudentLoan = model.StudentLoan;
                employee.UnionMember = model.UnionMember;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.State = model.State;
                employee.ZipCode = model.ZipCode;
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uplaodDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);

                    var webRootPath = _webHostEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmss") + fileName + extension;
                    var path = Path.Combine(webRootPath, uplaodDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                    employee.ImageUrl = "/" + uplaodDir + "/" + fileName;

                }

                await _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeDetailViewModel model = new EmployeeDetailViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                SocialSecurityNumber = employee.SocialSecurityNumber,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                State = employee.State,
                ImageUrl = employee.ImageUrl,
                ZipCode = employee.ZipCode

            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeDeleteViewModel()
            {
                Id = employee.Id,
                FullName = employee.FullName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task< IActionResult> Delete(EmployeeDeleteViewModel model)
        {
            await _employeeService.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
