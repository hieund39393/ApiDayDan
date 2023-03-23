using Authentication.Application.Commands.UserCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {

            RuleFor(x => x.UnitId).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn vị"));
            RuleFor(x => x.UserName).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên đăng nhập"));
            RuleFor(x => x.Email).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Email"));
            RuleFor(x => x.Name).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên đầy đủ"));
            RuleFor(x => x.Position).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Chức vụ"));
        }

    }
}
