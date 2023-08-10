using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.ApplicationUser;

namespace CraftBuddy.Web.ViewModels.User
{
	public class CrafterRegisterViewModel
	{
		[Required]
		[StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength, ErrorMessage = "The {0} must be at least {2} and maximum {1} characters long.")]
		[Display(Name = "Username")]
		public string Username { get; set; } = null!;

		[Required]
		[EmailAddress]
		[StringLength(EmailMaxLength, MinimumLength = EmailMinLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
		[Display(Name = "Email")]
		public string Email { get; set; } = null!;

		[Required]
		[StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; } = null!;

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; } = null!;

		[Required]
		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;
    }
}
