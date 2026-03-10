using System.ComponentModel;

namespace Eventify.Backend.Constants;

/// <summary>Šifarnici (referentne tablice) – koriste se za dropdown-e, filtere, tipove.</summary>
public enum ECodebook
{
    [Description("Category")]
    Category,

    [Description("EventType")]
    EventType,

    [Description("TicketType")]
    TicketType
}
