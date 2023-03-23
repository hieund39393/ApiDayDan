using Authentication.Application.Commands.RoleCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Infrastructure.Validations.Role
{
    public class RoleCreateValidator : AbstractValidator<RoleCreateOrUpdate>
    {
        public RoleCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên"));
        }
    }
}
