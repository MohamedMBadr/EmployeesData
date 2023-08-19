using Demo.DAL.Models;
using EmployeesData.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesData.Controllers
{
  //  [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager ,UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }



        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name,

                }).ToList();
                return View(roles);
            }
            else
            {
                var Role = await _roleManager.FindByNameAsync(name);
                if(Role is not null)
                {
                    var mappedRole = new RoleViewModel()
                    {
                        Id = Role.Id,
                        RoleName = Role.Name,
                    };

                    return View(new List<RoleViewModel>() { mappedRole });
                }
                return View(Enumerable.Empty<RoleViewModel>());

            }

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappeRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(mappeRole);
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }


        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var user = await _roleManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(user);

            return View(viewName, mappedRole);
        }



        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel UpdatedRole)
        {
            if (id != UpdatedRole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {

                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = UpdatedRole.RoleName;



                    await _roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(UpdatedRole);
        }




        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "delete");


        }

        [HttpPost(Name = "Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelet(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                await _roleManager.DeleteAsync(role);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }



        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role is null)
                return NotFound();
            ViewBag.RoleId = RoleId;
            var users =new List<UserInRoleVM>();
            foreach (var user in _userManager.Users)
            {
                var userInRole = new UserInRoleVM
                {
                    UserId = user.Id,
                    UserName =user.UserName,
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelect = true;
                else
                    userInRole.IsSelect = false;

                users.Add(userInRole);
                        

            }
            return View(users);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> models , string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var item in models)
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);

                    if (user is not null)
                    {
                        if (item.IsSelect && !(await _userManager.IsInRoleAsync(user, role.Name)))
                            await _userManager.AddToRoleAsync(user, role.Name);
                        else if(!item.IsSelect && (await _userManager.IsInRoleAsync(user ,role.Name)))
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                return RedirectToAction(nameof(Index), new {id = RoleId});
            }
            return View(models);

        }

    }
}
