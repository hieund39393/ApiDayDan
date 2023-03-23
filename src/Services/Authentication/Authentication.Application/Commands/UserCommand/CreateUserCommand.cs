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
        public Guid UnitId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid TeamId { get; set; }
        public int Position { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int? Gender { get; set; }
        public bool DefaultPassword { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
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

            User user = _mapper.Map<User>(request);
            user.Actived = true;
            user.CreatedDate = DateTime.Now;
            var unit = await _unitOfWork.UnitRepository.GetQuery(x => x.Id == request.UnitId).FirstOrDefaultAsync();
            if (unit == null)
            {
                throw new EvnException(Resources.MSG_NOT_FOUND, "Đơn vị");
            }
            user.Unit = unit;
            var data = await _userManager.FindByNameAsync(user.UserName);
            if (data == null)
            {
                var createResult = await _userManager.CreateAsync(user, "");
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
