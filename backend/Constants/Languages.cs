namespace Eventify.Backend.Constants;

using System.ComponentModel;

/// <summary>Jezici aplikacije (za višejezičnost).</summary>
public enum Languages
{
    [Description("English")]
    en = 0,

    [Description("Hrvatski jezik")]
    hr = 1,

    [Description("Bosanski jezik")]
    bs = 2,

    [Description("Srpski jezik")]
    sr = 3
}
