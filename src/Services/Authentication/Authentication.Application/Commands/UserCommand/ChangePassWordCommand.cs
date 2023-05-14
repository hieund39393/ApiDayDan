using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using EVN.Core.Extensions;
using EVN.Core.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;

namespace Authentication.Application.Commands.UserCommand
{
    public class ChangeMyPassWordCommand : IRequest<bool>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
    public class ChangeMyPassWordCommandHandler : IRequestHandler<ChangeMyPassWordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public ChangeMyPassWordCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<bool> Handle(ChangeMyPassWordCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(TokenExtensions.GetUserId());
            var data = await _unitOfWork.UserRepository.GetQuery(x => x.Id == userId).FirstOrDefaultAsync();
            if (data == null)
            {
                throw new EvnException(string.Format("Người dùng không tồn tại"));
            }
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                throw new EvnException(EvnResources.MSG_PASS_CONFIRMPASS_DO_NOT_MATCH);
            }
            // kiểm tra mật khẩu mới có trùng với mật khẩu hiện tại hay không 

            var userData = _userManager.ChangePasswordAsync(data, request.OldPassword, request.NewPassword).Result;
            if (userData.Succeeded == false)
            {
                var item = userData.Errors.FirstOrDefault().Code.ToString();
                if (item.Contains("PasswordMismatch"))
                {
                    throw new EvnException(string.Format("Mật khẩu cũ không đúng"));
                }
                else
                {
                    throw new EvnException(userData.Errors.Select(x => x.Code).ToString());
                }
            }

            await _userManager.UpdateAsync(data);
            return true;

        }



    }
}