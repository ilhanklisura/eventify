namespace Eventify.Backend.Models.Data.Util;

using System.ComponentModel;
using System.Reflection;

/// <summary>Dohvat [Description] atributa s enum vrijednosti.</summary>
public static class GetDescription
{
    public static string GetEnumDescription(Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());
        if (fi == null) return value.ToString();
        var attrs = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attrs is { Length: > 0 } ? attrs[0].Description : value.ToString();
    }
}
