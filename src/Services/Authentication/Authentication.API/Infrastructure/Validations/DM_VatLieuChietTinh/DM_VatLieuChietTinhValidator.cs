using Authentication.Application.Commands.DM_VatLieuChietTinhCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.DM_VatLieuChietTinh
{
    public class DM_VatLieuChietTinhValidator : AbstractValidator<CreateDM_VatLieuChietTinhCommand>
    {
        public DM_VatLieuChietTinhValidator()
        {
            RuleFor(x => x.TenVatLieuChietTinh).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Tên vật liệu chiết tinh"));
            RuleFor(x => x.DonViTinh).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn vị tính"));
        }
    }
}