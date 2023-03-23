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
using Microsoft.AspNetCore.Http;

namespace Authentication.Application.Commands.UserCommand
{

    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid TeamId { get; set; }
        public int Position { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int? Gender { get; set; }
        public bool DefaultPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string Code { get; set; }
        [Comment("Tên người dùng không dấu")]
        public string NameUnsigned { get; set; }
        public bool Actived { get; set; }
        /// <summary>
        /// Chọn ảnh đại diện
        /// </summary>
        public IFormFile file { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this._userService = userService;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = _mapper.Map<User>(request);
            var data = (_unitOfWork.UserRepository.GetQuery(c => c.Id == user.Id)).FirstOrDefault();
            if (data == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Người dùng"));
            }

            data.Avatar = user.Avatar ?? data.Avatar;
            data.UserName = user.UserName ?? data.UserName;
            data.Name = user.Name ?? data.Name;
            data.Gender = user.Gender ?? data.Gender;
            data.Email = user.Email ?? data.Email;
            data.PhoneNumber = user.PhoneNumber ?? data.PhoneNumber;
            data.Actived = user.Actived;
            _unitOfWork.UserRepository.Update(data);
            if (request.DefaultPassword == true)
            {
                await _userManager.RemovePasswordAsync(data);
                var createResult = await _userManager.AddPasswordAsync(data, "EVNHaNoi@12345");
                if (createResult.Succeeded)
                {
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            return true;
        }
    }
}
