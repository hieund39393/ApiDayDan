using Authentication.Application.Commands.DM_VatLieuCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.DM_VatLieu
{
    public class DM_VatLieuValidator : AbstractValidator<CreateDM_VatLieuCommand>
    {
        public DM_VatLieuValidator()
        {
            RuleFor(x => x.TenVatLieu).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên vật liệu"));
            RuleFor(x => x.DonViTinh).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn vị tính"));
        }
    }
}