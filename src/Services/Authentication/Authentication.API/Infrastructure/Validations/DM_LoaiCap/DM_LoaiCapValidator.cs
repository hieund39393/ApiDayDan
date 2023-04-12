using Authentication.Application.Commands.DM_LoaiCapCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.DM_LoaiCap
{
    public class DM_LoaiCapValidator : AbstractValidator<CreateDM_LoaiCapCommand>
    {
        public DM_LoaiCapValidator()
        {
            RuleFor(x => x.TenLoaiCap).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên loại cáp"));
            RuleFor(x => x.DonViTinh).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn vị tính"));
        }
    }
}