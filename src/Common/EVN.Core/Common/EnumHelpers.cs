using System;
using System.Collections.Generic;
using System.Linq;
using EVN.Core.Extensions;

namespace EVN.Core.Common
{
    public class EnumHelpers
    {
        public static List<EnumModels> ToEnumLists<T>()
        {
            var enumModels = new List<EnumModels>();
            var t = typeof(T);
            if (!t.IsEnum) return enumModels;

            var values = Enum.GetValues(t).Cast<Enum>().Where(x => !x.GetIgnore()).Select(e => new
            {
                Id = e.GetHashCode(),
                Name = e.GetDescription()
            });

            enumModels.AddRange(values.Select(item => new EnumModels {Id = item.Id, Name = item.Name}));
            return enumModels;
        }

        public static List<EnumModels> ToEnumLists<T>(List<int> elements)
        {
            var enumModels = new List<EnumModels>();
            var t = typeof(T);
            if (!t.IsEnum) return enumModels;

            var values = Enum.GetValues(t).Cast<Enum>().Where(x => !x.GetIgnore() && !elements.Contains(x.GetHashCode())).Select(e => new
            {
                Id = e.GetHashCode(),
                Name = e.GetDescription()
            });

            enumModels.AddRange(values.Select(item => new EnumModels { Id = item.Id, Name = item.Name }));
            return enumModels;
        }

        public static List<EnumModels> ToEnumDescriptionPlusLists<T>()
        {
            var enumModels = new List<EnumModels>();
            var t = typeof(T);
            if (!t.IsEnum) return enumModels;

            var values = Enum.GetValues(t).Cast<Enum>()
                .Where(x => !x.GetIgnore() && !string.IsNullOrEmpty(x.GetDescriptionPlus()))
                .Select(e => new
            {
                Id = e.GetHashCode(),
                Name = e.GetDescriptionPlus()
            });

            enumModels.AddRange(values.Select(item => new EnumModels {Id = item.Id, Name = item.Name}));
            return enumModels;
        }
    }

    public class EnumModels
    {
        /// <summary>
        /// Code
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name enum
        /// </summary>
        public string Name { get; set; }
    }
}