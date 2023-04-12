using Authentication.Application.Commands.DonGiaVatLieuCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.DonGiaVatLieu
{
    public class DonGiaVatLieuValidator : AbstractValidator<CreateDonGiaVatLieuCommand>
    {
        public DonGiaVatLieuValidator()
        {
            RuleFor(x => x.IdVatLieu).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Vật liệu"));
            RuleFor(x => x.DonGia).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn giá"));
            RuleFor(x => x.VanBan).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Văn bản"));
        }
    }
}