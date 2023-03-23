using AutoMapper;
using EVN.Core.Extensions;
using EVN.Core.Implements.Http;
using EVN.Core.Interfaces.Database;
using EVN.Core.Interfaces.Logging;
using MediatR;

namespace EVN.Core.Infrastructure.Handlers
{
    public abstract class BaseHandler
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;        
        protected readonly IAppLogger Logger;
        protected readonly IMediator Mediator;

        protected readonly string UserId;
        protected readonly string Token;

        protected BaseHandler()
        {
            UnitOfWork = HttpAppService.GetRequestService<IUnitOfWork>();
            Mapper = HttpAppService.GetRequestService<IMapper>();
            Logger = HttpAppService.GetRequestService<IAppLogger>();
            Mediator = HttpAppService.GetRequestService<IMediator>();

            UserId = TokenExtensions.GetUserId();
            Token = TokenExtensions.GetToken();
        }
    }
}
