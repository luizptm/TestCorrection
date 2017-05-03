using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestCorrection.Library;

namespace TestCorrection.ViewModels
{
	public class Login
	{
        [Required(ErrorMessage = "CPF obrigatório")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        [DataType(DataType.Text)]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		public string CPF { get; set; }

		[Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}

    public class LoginRegister
	{
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
        //[DataType("Email")]
        [DataType(DataType.EmailAddress)]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF obrigatório")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        [DataType(DataType.Text)]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
        public string CPF { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
        [DataType(DataType.Text)]
        public string Email { get; set; }
        
		[Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		[DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		public string Password { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		[DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		public string ConfirmPassword { get; set; }
	}

    public class LoginChangePassword
	{
        public int UserId { get; set; }

		[Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
        [DataType(DataType.Text)]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
        public string CPF { get; set; }

		[Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		[DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		[DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		public string NewPassword { get; set; }

		[Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		[DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(TestCorrection.Resources.Resources))]
		public string ConfirmPassword { get; set; }
	}
}