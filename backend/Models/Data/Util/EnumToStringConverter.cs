namespace Eventify.Backend.Models.Data.Util;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class EnumToStringConverter<T> : ValueConverter<T, string> where T : struct, Enum
{
    public EnumToStringConverter()
        : base(
            v => v.ToString(),
            v => Enum.Parse<T>(v))
    { }
}
