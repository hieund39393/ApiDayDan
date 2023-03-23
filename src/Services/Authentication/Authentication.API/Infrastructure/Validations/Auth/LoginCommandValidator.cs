using Authentication.Application.Commands.AuthCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validation.Auth
{
    /// <summary>
    /// Login Command Validator
    /// </summary>
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên đăng nhập"));
            RuleFor(x => x.Password).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Mật khẩu"));
        }
    }
}
