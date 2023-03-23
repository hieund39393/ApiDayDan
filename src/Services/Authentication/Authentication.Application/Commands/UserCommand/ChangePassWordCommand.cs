using Authentication.Application.Services;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using AutoMapper;
using EVN.Core.Exceptions;
using EVN.Core.Properties;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Authentication.Application.Commands.UserCommand
{
    public class ChangePassWordCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string OldPassWord { get; set; }
        public string NewPassWord { get; set; }
        public string ComfirmNewPassWord { get; set; }
    }
    public class ChangePassWordCommandHandler : IRequestHandler<ChangePassWordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public ChangePassWordCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<bool> Handle(ChangePassWordCommand request, CancellationToken cancellationToken)
        {
            var data = (_unitOfWork.UserRepository.GetQuery(x => x.Id == request.Id)).FirstOrDefault();
            if (data == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Người dùng"));
            }
            if(request.NewPassWord != request.ComfirmNewPassWord)
            {
                throw new EvnException(EvnResources.MSG_PASS_CONFIRMPASS_DO_NOT_MATCH);
            }
            var userData = _userManager.ChangePasswordAsync(data, request.OldPassWord, request.NewPassWord).Result;
            if (userData.Succeeded == false)
            {
                throw new EvnException(userData.Errors.ToString());
            }
            await _userManager.UpdateAsync(data);
            return true;
        }
    }
}
