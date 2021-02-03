using System;
using System.Linq;
using UModFramework.API;

namespace DysonsGalaxy.CustomParser
{

    /// <summary>
    /// This is not used anywhere, but should serve as a somewhat okay-ish example for a custom parser so it stays
    /// </summary>
    public class CustomUMFConfigSbyte : UMFConfigParser<sbyte>
    {
        public sbyte DefaultValue;
        public sbyte MinValue;
        public sbyte MaxValue;
        public sbyte VanillaValue;
        public bool RequiresRestart;
        public sbyte[] AllowedValues;

        public CustomUMFConfigSbyte(
          sbyte defaultValue = 0,
          sbyte minValue = 0,
          sbyte maxValue = 0,
          sbyte vanillaValue = 0,
          bool requiresRestart = false,
          params sbyte[] allowedValues)
        {
            DefaultValue = defaultValue;
            MinValue = Math.Min(minValue, maxValue);
            MaxValue = Math.Max(minValue, maxValue);
            VanillaValue = vanillaValue;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override sbyte Parse(string value)
        {
            if (!sbyte.TryParse(value, out var result))
                result = DefaultValue;
            return result;
        }

        public override string ToString() => DefaultValue.ToString();

        public override string Default() => DefaultValue.ToString();

        public override string Range() => MaxValue == 0 ? null : MinValue + "," + MaxValue;

        public override string Vanilla() => VanillaValue == 0 ? null : VanillaValue.ToString();

        public override string Allowed() => AllowedValues.Length == 0 ? null : string.Join(",", AllowedValues.Select(x => x.ToString()).ToArray());

        public override string Restart() => RequiresRestart.ToString();
    }
}