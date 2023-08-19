using Demo.BLL.InterFaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Data;
using System.Linq;

namespace Demo.pl.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {

            var Employess= _employeeRepository.GetAll();
            return View(Employess);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            // Employee.Languagelevel = string.Join(", ",Employee.Languagelevel);
             int age= (int)DateTime.Now.Year - Employee.BirthBade.Year;
            Employee.Age= age;

            if (ModelState.IsValid)
            {

                _employeeRepository.Add(Employee);
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        [HttpGet]

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Employee = _employeeRepository.Get(id.Value);
            if (Employee is null)
                return NotFound();

            return View(viewName, Employee);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee Employee)
        {
            if (id != Employee.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Update(Employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(Employee);
        }


        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");


        }

        [Authorize(Roles = "Admin")]

        [HttpPost]

        public IActionResult Delete(Employee Employee)
        {
            try
            {
                _employeeRepository.Delete(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(Employee);
            }


        }
    }
}
