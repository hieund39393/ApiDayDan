using Authentication.Application.Commands.DonGiaNhanCongCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.DonGiaNhanCong
{
    public class DonGiaNhanCongValidator : AbstractValidator<CreateDonGiaNhanCongCommand>
    {
        public DonGiaNhanCongValidator()
        {
            RuleFor(x => x.DonGia).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn giá"));
            RuleFor(x => x.DinhMuc).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Định mức"));
        }
    }
}