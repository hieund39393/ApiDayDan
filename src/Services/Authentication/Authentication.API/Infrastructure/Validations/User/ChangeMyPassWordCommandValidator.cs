using Authentication.Application.Commands.UserCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;
using System;

namespace Authentication.API.Infrastructure.Validations.User
{
    //public class ChangeMyPassWordCommandValidator : AbstractValidator<ChangeMyPassWordCommand>
    //{
    //    public ChangeMyPassWordCommandValidator()
    //    {
    //        RuleFor(x => x.NewPassword).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Mật khẩu"));
    //        RuleFor(x => x.NewPassword).MinimumLength(6).WithMessage(Resources.MSG_PASSWORD_MIN_LENGTH).MaximumLength(16).WithMessage(Resources.MSG_PASSWORD_MAX_LENGTH);
    //        RuleFor(x => x.NewPassword).Matches(@"[A-Z]").WithMessage(string.Format(Resources.MSG_REQUIRED_PASSWORD, "chữ cái viết hoa."));
    //        RuleFor(x => x.NewPassword).Matches(@"[0-9]").WithMessage(string.Format(Resources.MSG_REQUIRED_PASSWORD, "số."));
    //        RuleFor(x => x.NewPassword).Equal(x => x.ComfirmNewPassword, StringComparer.CurrentCulture).WithMessage(string.Format(Resources.MSG_PASS_CONFIRMPASS_DO_NOT_MATCH));
    //        RuleFor(x => x.ComfirmNewPassword).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Xác nhận mật khẩu"));
    //    }
    //}
}
