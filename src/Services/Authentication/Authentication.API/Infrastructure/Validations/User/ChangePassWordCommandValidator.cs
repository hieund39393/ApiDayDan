using Authentication.Application.Commands.UserCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.User
{
    public class ChangePassWordCommandValidator : AbstractValidator<ChangeMyPassWordCommand>
    {
        public ChangePassWordCommandValidator()
        {

        }
    }
}
