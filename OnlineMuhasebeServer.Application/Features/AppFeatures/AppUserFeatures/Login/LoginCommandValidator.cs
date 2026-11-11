using FluentValidation;

namespace OnlineMuhasebeServer.Application.Features.AppFeatures.AppUserFeatures.Login
{
	public sealed class LoginCommandValidator:AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(p => p.EmailOrUserName).NotNull().WithMessage("Mail ya da kullanıcı adı yazmalısınız");
			RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre bilgisi boş olamaz");

			RuleFor(p => p.Password).NotNull().WithMessage("Şifre bilgisi boş olamaz");
			RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre bilgisi boş olamaz");
			RuleFor(p => p.Password).MinimumLength(6).WithMessage("Şifre bilgisi boş olamaz");
			RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifreniz en az bir adet büyük harf içermelidir");
			RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifreniz en az bir adet küçük harf içermelidir");
			RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifreniz en az bir adet satı içermelidir");
			RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifreniz en az bir adet özel karakter içermelidir");
			
		}
	}
}
