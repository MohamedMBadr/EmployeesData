using System.ComponentModel.DataAnnotations;

namespace EmployeesData.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email is Required ")]
		[EmailAddress(ErrorMessage = "Email not Valid")]
		public string Email { get; set; }


        public string PhoneNumber { get; set; }


		[Required(ErrorMessage = "Passwod is Required ")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}

	}
