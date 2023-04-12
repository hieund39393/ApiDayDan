using Authentication.Application.Commands.GiaCapCommand;
using Authentication.Infrastructure.Properties;
using FluentValidation;

namespace Authentication.API.Infrastructure.Validations.GiaCap
{
    public class GiaCapValidator : AbstractValidator<CreateGiaCapCommand>
    {
        public GiaCapValidator()
        {
            RuleFor(x => x.IdLoaiCap).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Loại cáp"));
            RuleFor(x => x.DonGia).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Đơn giá"));
            RuleFor(x => x.VanBan).NotEmpty().WithMessage(string.Format(Resources.MSG_REQUIRED_FIELD, "Văn bản"));
        }
    }
}