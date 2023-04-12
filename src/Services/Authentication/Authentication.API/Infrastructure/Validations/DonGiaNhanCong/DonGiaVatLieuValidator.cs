using Authentication.Application.Commands.DonGiaNhanCongCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.DonGiaNhanCong
{
    public class DonGiaNhanCongValidator : AbstractValidator<CreateDonGiaNhanCongCommand>
    {
        public DonGiaNhanCongValidator()
        {
            RuleFor(x => x.HeSo).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Nhân công"));
            RuleFor(x => x.DonGia).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn giá"));
            RuleFor(x => x.CapBac).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Cấp bậc"));
            RuleFor(x => x.IdVung).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Vùng"));
            RuleFor(x => x.IdKhuVuc).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Khu vực"));
        }
    }
}