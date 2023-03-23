using Authentication.Application.Commands.ModuleCommand;
using Authentication.Application.Commands.RoleCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Infrastructure.Validations.Module
{
    public class ModuleCreateOrUpdateValidator : AbstractValidator<ModuleCreateOrUpdate>
    {
        public ModuleCreateOrUpdateValidator()
        {
            RuleFor(x => x.ModuleName).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên module"));
            RuleFor(x => x.ModuleCode).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Mã module"));
            RuleFor(x => x.TenVietTat).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên viết tắt"));
        }
    }
}
