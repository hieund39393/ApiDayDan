using EVN.Core.Implements.Http;
using MediatR;

namespace EVN.Core.Infrastructure.Services
{
    public abstract class BaseService
    {
        protected readonly IMediator Mediator;

        protected BaseService()
        {
            Mediator = HttpAppService.GetRequestService<IMediator>();
        }
    }
}
