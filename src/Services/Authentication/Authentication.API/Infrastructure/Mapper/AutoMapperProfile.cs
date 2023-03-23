using Authentication.Application.Commands.ModuleCommand;
using Authentication.Application.Commands.RoleCommand;
using Authentication.Application.Commands.UserCommand;
using Authentication.Application.Model.Role;
using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using AutoMapper;

namespace Authentication.API.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<RoleCreateOrUpdate, Role>();
            CreateMap<ModuleCreateOrUpdate, Module>();
            CreateMap<User, GetUserRoleResponse>();
        }
    }
}
