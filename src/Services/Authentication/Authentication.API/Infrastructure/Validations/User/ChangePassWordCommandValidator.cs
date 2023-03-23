using Authentication.Application.Commands.UserCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.User
{
    public class ChangePassWordCommandValidator : AbstractValidator<ChangePassWordCommand>
    {
        public ChangePassWordCommandValidator()
        {
            RuleFor(x => x.NewPassWord).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Mật khẩu"))
                  .MinimumLength(6).WithMessage(Resources.MSG_PASSWORD_MIN_LENGTH)
                  .MaximumLength(16).WithMessage(Resources.MSG_PASSWORD_MAX_LENGTH)
                  .Matches(@"[A-Z]").WithMessage(string.Format(Resources.MSG_REQUIRED_PASSWORD, "chữ cái viết hoa."))
                  .Matches(@"[0-9]").WithMessage(string.Format(Resources.MSG_REQUIRED_PASSWORD, "số."))
                  .Equal(x=>x.ComfirmNewPassWord).WithMessage(string.Format(Resources.MSG_PASS_CONFIRMPASS_DO_NOT_MATCH));
            RuleFor(x => x.ComfirmNewPassWord).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Xác nhận mật khẩu"));

        }
    }
}
