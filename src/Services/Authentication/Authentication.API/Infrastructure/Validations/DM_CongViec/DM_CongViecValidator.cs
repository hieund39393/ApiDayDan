using Authentication.Application.Commands.DM_CongViecCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.DM_CongViec
{
    public class DM_CongViecValidator : AbstractValidator<CreateDM_CongViecCommand>
    {
        public DM_CongViecValidator()
        {
            RuleFor(x => x.TenCongViec).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên công việc"));
            RuleFor(x => x.DonViTinh).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn vị tính"));
        }
    }
}