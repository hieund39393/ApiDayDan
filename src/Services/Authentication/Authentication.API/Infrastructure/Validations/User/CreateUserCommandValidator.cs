using Authentication.Application.Commands.AuthCommand;
using Authentication.Application.Commands.UserCommand;
using Authentication.Application.Queries;
using Authentication.Infrastructure.Properties;
using EVN.Core.Properties;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Infrastructure.Validations.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {

            RuleFor(x => x.UnitId).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn vị"));
            RuleFor(x => x.Name).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên đầy đủ"));
            RuleFor(x => x.Email).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Email"));
            RuleFor(x => x.Position).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Chức vụ"));
            RuleFor(x => x.UserName).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên đăng nhập"));
            RuleFor(x => new { x.SSO, x.DefaultPassword, x.Password, x.ComfirmPassword }).Custom((obj, context) =>
             {
                 if (obj.DefaultPassword == false && obj.SSO == false)
                 {
                     if (string.IsNullOrWhiteSpace(obj.Password))
                     {
                         context.AddFailure(string.Format(Resources.MSG_REQUIRED_FIELD, "Mật khẩu"));
                     }
                     if (string.IsNullOrEmpty(obj.ComfirmPassword))
                     {
                         context.AddFailure(string.Format(Resources.MSG_REQUIRED_FIELD, "Xác nhận mật khẩu"));
                     }
                     if (obj.Password != obj.ComfirmPassword)
                     {
                         context.AddFailure(string.Format(Resources.MSG_PASS_CONFIRMPASS_DO_NOT_MATCH));
                     }
                 }
             });
        }

    }
}
