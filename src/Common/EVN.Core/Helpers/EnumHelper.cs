using EVN.Core.Extensions;
using EVN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EVN.Core.Helpers
{
    public class EnumHelper
    {
        public static List<EnumModel> ToList<T>()
        {
            var enumModels = new List<EnumModel>();
            var t = typeof(T);
            if (!t.IsEnum) return enumModels;

            var values = Enum.GetValues(t).Cast<Enum>().Where(x => !x.GetIgnore()).Select(e => new
            {
                Id = e.GetHashCode(),
                Name = e.GetDescription()
            });

            enumModels.AddRange(values.Select(item => new EnumModel { Id = item.Id, Name = item.Name }));
            return enumModels;
        }
    }
}
