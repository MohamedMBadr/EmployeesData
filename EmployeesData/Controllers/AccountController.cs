using Demo.DAL.Models;
using EmployeesData.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeesData.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region Register

        //Account/Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FName= model.FName,
                    LName= model.LName,
                    UserName = model.Email.Split('@')[0],
                    Email= model.Email.ToLower(),
                    PhoneNumber = model.PhoneNumber,
                    IsAgree= model.IsAgree
                };

                var result =await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }



        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
                var user =await _userManager.FindByEmailAsync(model.Email.ToLower());
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result =await _signInManager.PasswordSignInAsync(user ,model.Password, model.RememberMe,false);
                        if (result.Succeeded)
                            return RedirectToAction("index", "Home");
                        

                        

                    }
                    ModelState.AddModelError(string.Empty, "password is Not correct ");

                }
                ModelState.AddModelError(string.Empty, "Email is not Exist");
                

			}
            return View(model);

		}
        #endregion

        #region Sign Out
        public  async Task<IActionResult> SignOut() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


        #endregion


      
    }
}
