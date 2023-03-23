using EVN.Core.SeedWork;
using System;

namespace Authentication.Application.Model.User
{
    public class UserRequest : PagingQuery
    {
        public Guid UnitId { get; set; }
    }
}
