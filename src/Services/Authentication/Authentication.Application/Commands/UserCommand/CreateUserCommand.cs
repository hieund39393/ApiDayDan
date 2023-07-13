using Authentication.Application.Services;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using AutoMapper;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Authentication.Application.Commands.UserCommand
{

    public class CreateUserCommand : IRequest<bool>
    {
        public bool SSO { get; set; }
        public Guid? PositionId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool DefaultPassword { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this._userService = userService;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            User user = new User();
            user.Actived = true;
            user.UserName = request.UserName;
            user.Name = request.Name;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.PositionId = request.PositionId;
            var password = request.Password;
            var data = await _userManager.FindByNameAsync(user.UserName);
            if (data == null)
            {
                var createResult = await _userManager.CreateAsync(user, password);
                if (createResult.Succeeded)
                {
                    System.Console.WriteLine($"createResult.Succeeded = {createResult.Succeeded}");
                }
                else
                {
                    throw new EvnException($"createResult.Errors = {Newtonsoft.Json.JsonConvert.SerializeObject(createResult.Errors)}");
                }
            }
            else
            {
                throw new EvnException(Resources.USERNAME_AVAILABLE);
            }
            return true;

        }
    }
}
