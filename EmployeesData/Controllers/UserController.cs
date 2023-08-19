using AutoMapper;
using Demo.DAL.Models;
using EmployeesData.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesData.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser > userManager , SignInManager<ApplicationUser> signInManager
			, IMapper  mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }

		public async Task<IActionResult> Index(string Email)
		{
			if (string.IsNullOrEmpty(Email))
			{
				var users = _userManager.Users.Select( u => new UserViewModel()
				{
					Id = u.Id,
					FName= u.FName,
					LName= u.LName,
					Email= u.Email,
					PhoneNumber =u.PhoneNumber,
					Roles = _userManager.GetRolesAsync(u).Result
				}).ToList();
				return View(users);
			}
			else
			{
				var user =await _userManager.FindByEmailAsync(Email);
				var mappedUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};

				return View(new List<UserViewModel>() { mappedUser}) ;
			}

		}


        public async Task< IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

			var mappedUser = _mapper.Map<ApplicationUser ,UserViewModel>(user);

            return View(viewName, mappedUser);
        }



        public async Task< IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel UpdatedUser)
        {
            if (id != UpdatedUser.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {

                    var user = await _userManager.FindByIdAsync(id);
                    user.FName = UpdatedUser.FName;
                    user.LName = UpdatedUser.LName;
                    user.PhoneNumber = UpdatedUser.PhoneNumber;

                    await _userManager.UpdateAsync(user);
					
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(UpdatedUser);
        }




        public async Task< IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");


        }
        [HttpPost(Name = "Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelet(string id )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }


        }
    }
}
