using System.ComponentModel.DataAnnotations;

namespace EmployeesData.ViewModels
{
	public class RegisterViewModel
	{

		public string FName { get; set; }
		public string LName { get; set; }

		[Required(ErrorMessage = "Email is Required ")]
		[EmailAddress(ErrorMessage = "Email not Valid")]
		public string Email { get; set; }


		public string PhoneNumber { get; set; }



		[Required(ErrorMessage = "Passwod is Required ")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Passwod is Required ")]
		[Compare("Password", ErrorMessage = "Confirm password does not match password")]
		[DataType(DataType.Password)]
		public string ConfirmPasswod { get; set; }


		public bool IsAgree { get; set; }
	}
}
