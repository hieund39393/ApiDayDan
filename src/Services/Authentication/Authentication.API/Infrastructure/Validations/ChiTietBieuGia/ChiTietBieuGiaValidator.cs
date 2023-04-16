using Authentication.Application.Commands.ChiTietBieuGiaCommand;
using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.ChiTietBieuGia
{
    public class ChiTietBieuGiaValidator : AbstractValidator<ChiTietBieuGiaRequest>
    {
        public ChiTietBieuGiaValidator()
        {
            RuleFor(x => x.Quy).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Quý"));
            RuleFor(x => x.Nam).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Năm"));
            RuleFor(x => x.IdBieuGia).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Chi tiết biểu giá"));
        }
    }
}