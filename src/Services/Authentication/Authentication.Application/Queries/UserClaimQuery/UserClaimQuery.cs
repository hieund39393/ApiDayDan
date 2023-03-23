using EVN.Core.Extensions;
using EVN.Core.Models.Interface;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using EVN.Core.Common;

namespace Authentication.Application.Queries.UserClaimQuery
{
    public interface IUserClaimQuery
    {
        Task<List<Guid>> GetListTeamByUserClaim();
    }
    public class UserClaimQuery : IUserClaimQuery
    {
        private readonly IRepository<UserClaim> _userRep;
        public UserClaimQuery(IRepository<UserClaim> userRep)
        {
            _userRep = userRep;
        }

        public async Task<List<Guid>> GetListTeamByUserClaim()
        {
            var userId = Guid.Parse(TokenExtensions.GetUserId());
            var listTeam = _userRep.GetQuery(x => x.UserId == userId && x.ClaimType == AppConstants.ClaimType.TeamId)
                .Select(x => Guid.Parse(x.ClaimValue)).ToList();
            return listTeam;
        }
    }
}
