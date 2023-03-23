using EVN.Core.Attributes;
using EVN.Core.Properties;

namespace Authentication.Infrastructure.AggregatesModel.UserAggregate
{
    public enum UserPosition
    {
        [Localization(nameof(EvnResources.MSG_ADMINISTRATOR), typeof(EvnResources))]
        Administrator = 0,
        #region Cấp đơn vị
        [Localization(nameof(EvnResources.MSG_PRESIDENT), typeof(EvnResources))]
        Director = 1,
        [Localization(nameof(EvnResources.MSG_MANAGER), typeof(EvnResources))]
        Manager = 2,
        [Localization(nameof(EvnResources.MSG_TEAM_LEAD), typeof(EvnResources))]
        TeamLead = 3,
        [Localization(nameof(EvnResources.MSG_WORKER), typeof(EvnResources))]
        Worker = 4,
        [Localization(nameof(EvnResources.MSG_EMPLOYEE), typeof(EvnResources))]
        Employee = 5,
        #endregion
        #region Cấp tổng công ty
        [Localization(nameof(EvnResources.MSG_CHAIRMAN), typeof(EvnResources))]
        Chairman = 6,
        [Localization(nameof(EvnResources.MSG_VICE_CHAIRMAN), typeof(EvnResources))]
        ViceChairman = 7,
        [Localization(nameof(EvnResources.MSG_TECHNICAL_CHIEF), typeof(EvnResources))]
        TechnicalChief = 8,
        [Localization(nameof(EvnResources.MSG_DEPUTY_CHIEF), typeof(EvnResources))]
        DeputyChief = 9,
        [Localization(nameof(EvnResources.MSG_EXPERT), typeof(EvnResources))]
        Expert = 10,
        #endregion

    }
}
